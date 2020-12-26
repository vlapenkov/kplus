import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { CarsListComponent } from './components/cars-list/cars-list.component';
import { EditCarComponent } from './components/edit-car/edit-car.component';
import { LoaderComponent } from './components/loader/loader.component';
import {NgxPaginationModule} from 'ngx-pagination';
import {ListsResolveService} from './services/lists-resolve.service';
import {routes} from './routes';
import { NotFoundComponent } from './components/not-found/not-found.component';




@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    CarsListComponent,
    EditCarComponent,
    LoaderComponent,
    NotFoundComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    NgxPaginationModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
