import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AboutComponent } from "./about/about.component";
import { DishAddComponent } from "./dishes/dish-add/dish-add.component";
import { DishDetailComponent } from "./dishes/dish-detail/dish-detail.component";
import { DishUpdateComponent } from "./dishes/dish-update/dish-update.component";
import { DishesComponent } from "./dishes/dishes.component";
import { IngredientAddComponent } from "./dishes/ingredients/ingredient-add/ingredient-add.component";
import { IngredientUpdateComponent } from "./dishes/ingredients/ingredient-update/ingredient-update.component";
// import { SigninOidcComponent } from "./signin-oidc/signin-oidc.component";

export const routes: Routes = [
  // redirect root to the dasbhoard route
  {
    path: "",
    redirectTo: "dishes",
    pathMatch: "full",
    // canActivate: [RequireAuthenticatedUserRouteGuardService],
  },
  {
    path: "dishes",
    component: DishesComponent,
    // canActivate: [RequireAuthenticatedUserRouteGuardService],
  },
  { path: "about", component: AboutComponent },
  {
    path: "dish-add",
    component: DishAddComponent,
    // canActivate: [RequireAuthenticatedUserRouteGuardService],
  },
  {
    path: "dishes/:dishId",
    component: DishDetailComponent,
    // canActivate: [RequireAuthenticatedUserRouteGuardService],
  },
  {
    path: "dish-update/:dishId",
    component: DishUpdateComponent,
    // canActivate: [RequireAuthenticatedUserRouteGuardService],
  },
  {
    path: "dishes/:dishId/ingredient-add",
    component: IngredientAddComponent,
    // canActivate: [RequireAuthenticatedUserRouteGuardService],
  },
  {
    path: "dishes/:dishId/ingredient-update/:ingredientId",
    component: IngredientUpdateComponent,
    // canActivate: [RequireAuthenticatedUserRouteGuardService],
  },
  // { path: "signin-oidc", component: SigninOidcComponent },
  // { path: "redirect-silentrenew", component: RedirectSilentRenewComponent },
  // //  { path: '**', redirectTo: 'dishes' },
];

// define a module
@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      onSameUrlNavigation: "reload",
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
