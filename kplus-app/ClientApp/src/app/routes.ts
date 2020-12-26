import {CarsListComponent} from './components/cars-list/cars-list.component';
import {CounterComponent} from './components/counter/counter.component';
import {EditCarComponent} from './components/edit-car/edit-car.component';
import {ListsResolveService} from './services/lists-resolve.service';
import {NotFoundComponent} from './components/not-found/not-found.component';

export const routes = [
  { path: '', component: CarsListComponent, pathMatch: 'full' },
  { path: 'counter', component: CounterComponent },
  { path: 'edit-car',
    component: EditCarComponent,
    data: {  title: 'Добавление автомобиля'},
    resolve: {
      lists: ListsResolveService
    }
  },
  { path: 'edit-car/:id', component: EditCarComponent ,
    data: {  title: 'Редактирование автомобиля'},
    resolve: {
      lists: ListsResolveService
    }
  },
  { path: '**', component: NotFoundComponent },
];

