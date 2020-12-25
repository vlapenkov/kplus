export interface ICarListItem {
  id: string;
  brand: {id: number, name: string };
  modelName: string;
  created: string;
  chassisType: {id: number, name: string };
  seatsCount: number;
  url: string;
  urlImage : string;
}
