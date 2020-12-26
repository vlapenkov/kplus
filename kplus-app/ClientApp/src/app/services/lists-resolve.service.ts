import { Injectable } from '@angular/core';
import {ListsService} from './lists-service.service';
import {ActivatedRouteSnapshot, Resolve, Router, RouterStateSnapshot} from '@angular/router';
import {IListItem} from '../interfaces/IListItem';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ListsResolveService implements Resolve<[IListItem[], IListItem[]] | null> {

  constructor(
    private _listService: ListsService,
    private _router: Router
  ) {  }

  public resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<[IListItem[], IListItem[]] | null>
  {
    return this._listService.getLists();
  }

}
