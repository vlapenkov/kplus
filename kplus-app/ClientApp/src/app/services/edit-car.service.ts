import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ICarListItem} from '../interfaces/ICarListItem';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EditCarService {

  constructor(@Inject('BASE_URL') private baseUrl: string, private http: HttpClient) { }

 public getCarData(id: string): Observable<ICarListItem>
  {
    return this.http.get<ICarListItem>(`${this.baseUrl}api/cars/${id}`);
  }

 public postCarData(id: string,body: any)
  {
    if (!id)
    return this.http.post<ICarListItem>(`${this.baseUrl}api/cars`, body );
    else
      return this.http.put<ICarListItem>(`${this.baseUrl}api/cars/${id}`, body );
  }


}
