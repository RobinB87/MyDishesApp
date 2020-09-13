import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StoreModule } from '@ngrx/store';
import { AppComponent } from './app.component';
import { DishReducer } from './core/store/dish/dish.reducer';
import { DishAddComponent } from './dish/dish-add';
import { DishListComponent } from './dish/dish-list';

@NgModule({
  declarations: [AppComponent, DishListComponent, DishAddComponent],
  imports: [BrowserModule, StoreModule.forRoot({ customers: DishReducer })],
  providers: [],
  bootstrap: [AppComponent],
})
export class AppModule {}
