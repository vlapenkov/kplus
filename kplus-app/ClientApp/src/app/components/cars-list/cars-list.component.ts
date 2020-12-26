import {Component, Inject, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ICarListItem} from '../../interfaces/ICarListItem';
import {ICarsResponse} from '../../interfaces/ICarsResponse';


@Component({
  selector: 'app-cars-list',
  templateUrl: './cars-list.component.html',
  styleUrls: ['./cars-list.component.css']
})
export class CarsListComponent implements OnInit {

  carItems: ICarListItem[];
  totalItems = 1;
  p = 1;

  constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {}
  ngOnInit(): void {
    this.loadPage(1);
  }

loadPage(page) {
  this.http.get<ICarsResponse>(`${this.baseUrl}api/cars`, {params: {
      page

    }}).subscribe((response: ICarsResponse) => { this.carItems = response.cars; this.totalItems = response.total; });
}

  delete(id: string) {
    this.http.delete(`${this.baseUrl}api/cars/${id}`).subscribe((response) => { this.p = 1; this.loadPage(1); });
  }

}




