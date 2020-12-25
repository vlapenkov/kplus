import {ICarListItem} from './ICarListItem';

export interface ICarsResponse {
  total: number;
  cars: ICarListItem[];
}
