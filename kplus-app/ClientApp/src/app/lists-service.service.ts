import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, forkJoin} from 'rxjs';
import {IListItem} from './interfaces/IListItem';

@Injectable({
  providedIn: 'root'
})
export class ListsService {

  constructor(@Inject('BASE_URL') private baseUrl: string, private http: HttpClient) { }


  public getLists(): Observable<[IListItem[], IListItem[]]>
  {
    const brands = this.http.get<IListItem[]>(this.baseUrl + 'api/brands');
    const chassis = this.http.get<IListItem[]>(this.baseUrl + 'api/ChassisType');

   return  forkJoin([brands, chassis]);
  }
}
