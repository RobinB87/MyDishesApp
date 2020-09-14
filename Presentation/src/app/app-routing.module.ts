import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DishDetailComponent } from './dish/dish-detail/dish-detail.component';
import { DishListComponent } from './dish/dish-list';

const routes: Routes = [
  { path: 'dishes', component: DishListComponent },
  { path: 'dish/:dishId', component: DishDetailComponent },
  { path: '', redirectTo: '/dishes', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
