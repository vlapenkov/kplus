import {Component, Inject, OnInit} from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {IListItem} from '../../interfaces/IListItem';
import {ICarListItem} from '../../interfaces/ICarListItem';
import {FormBuilder,  FormGroup, Validators} from '@angular/forms';
import {EditCarService} from '../../services/edit-car.service';
import {ListsService} from '../../services/lists-service.service';

@Component({
  selector: 'app-edit-car',
  templateUrl: './edit-car.component.html',
  styleUrls: ['./edit-car.component.css']
})
export class EditCarComponent implements OnInit {
  private id: string;
  private brands: IListItem[];
  private chassis: IListItem[];
  private activeCar: ICarListItem;
  private isLoading = true;
  private isUpdated = false;
  private urlImage: string  = null;
  private  title: string;

  get urlPath() {
    return  this.urlImage || '/assets/200_100.png';
  }


 private formCar: FormGroup;


  constructor(private _route: ActivatedRoute,
              private fb: FormBuilder,
              private _router: Router,
              private _carService: EditCarService,
              private _listService: ListsService) { }

  ngOnInit() {

   this.id = this._route.snapshot.params['id'];
    this.title = this._route.snapshot.data.title;
    [this.brands, this.chassis] = this._route.snapshot.data.lists;


    this.loadEmpty();
    if (this.id) {
    this.loadCarData();
    } else {
     this.isLoading = false;
    }

  }



  loadCarData() {
    this._carService.getCarData(this.id)
   // this.http.get<ICarListItem>(`${this.baseUrl}api/cars/${this.id}`)
      .subscribe(response => {
      this.activeCar = response;
      this.formCar.patchValue({
        'brand': this.activeCar.brand.id,
        'chassis': this.activeCar.chassisType.id,
        'modelName': this.activeCar.modelName,
        'seatsCount': this.activeCar.seatsCount,
        'url': this.activeCar.url ,
        'urlImage': this.activeCar.urlImage
      });


      if (this.activeCar.urlImage) {
      this.urlImage = this.activeCar.urlImage;
      }
      this.isLoading = false;
    });

  }

  loadEmpty(): void {

    this.formCar = this.fb.group(
      {'brand': [0, [Validators.required, Validators.min(1)]],
        'chassis': [0, [Validators.required, Validators.min(1)]],
        'modelName': ['', [Validators.required]],
        'seatsCount': [4, [Validators.required, Validators.min(1), Validators.max(12)]],
        'url': ['' ],
    //    'url': ['', [ Validators.pattern(/^https?:\/\/(\w+)\.ru$/)]],
        'urlImage': [this.urlImage, Validators.required]
      },
    );

  }

 prepareRequest(): any {

   const body = { ...this.formCar.value, ...{brandId: this.formCar.get('brand').value, chassisTypeId: this.formCar.get('chassis').value}};

    return body;
 }

  save() {
    this.isUpdated = true;
   const body = this.prepareRequest();
   this._carService.postCarData(this.id, body).subscribe(
     response => { this.processSuccess(); },
     (errorResponse: HttpErrorResponse) => this.processError(errorResponse)
     );
  }

  selectFile(evt) {
    if (evt.target.files && evt.target.files[0]) {
      const reader = new FileReader();
      reader.readAsDataURL(evt.target.files[0]); // read file as data url
      reader.onload = () => {
        this.urlImage = reader.result as string;
        this.formCar.patchValue({
          urlImage: reader.result
        });
      };
    }
  }


  processSuccess() {
    this._router.navigateByUrl('/');
  }


  processError(errorResponse: HttpErrorResponse) {
    {
      this.isUpdated = false;
      console.error(errorResponse);
      // validation error
      if (errorResponse.status === 400) {
        this.formCar.setErrors({'error400': {title: errorResponse.error.Title}});
      } else {
        this.formCar.setErrors({'error500': {title: errorResponse.error.Title}});
      }
    }


  }
}

