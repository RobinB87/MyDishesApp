import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogInComponent } from './auth/log-in/log-in.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { DishDetailComponent } from './dish/dish-detail/dish-detail.component';
import { DishListComponent } from './dish/dish-list';

const routes: Routes = [
  { path: 'log-in', component: LogInComponent },
  { path: 'sign-up', component: SignUpComponent },
  { path: 'dishes', component: DishListComponent },
  { path: 'dish/:dishId', component: DishDetailComponent },
  { path: '', redirectTo: '/dishes', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
