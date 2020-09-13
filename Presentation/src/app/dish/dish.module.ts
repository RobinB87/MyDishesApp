import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';
import { DishRoutingModule } from './dish-routing.module';
import { DishComponent } from './dishes/dish.component';
import { DishListComponent } from './dish-list/dish-list.component';
import { DishDetailsComponent } from './dish-details/dish-details.component';

@NgModule({
  imports: [CommonModule, SharedModule, DishRoutingModule],
  exports: [DishComponent],
  declarations: [DishComponent, DishListComponent, DishDetailsComponent],
  providers: [],
})
export class DishesModule {}
