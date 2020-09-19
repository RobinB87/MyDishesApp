import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LogInComponent } from './components/auth/log-in';
import { SignUpComponent } from './components/auth/sign-up';
import { DishDetailComponent } from './components/dish/dish-detail';
import { DishListComponent } from './components/dish/dish-list';

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
