import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DishListComponent } from './dish/dish-list';

const routes: Routes = [
  { path: 'dishes', component: DishListComponent },
  { path: '', redirectTo: '/dishes', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
