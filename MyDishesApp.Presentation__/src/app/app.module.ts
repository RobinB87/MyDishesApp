import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule, MatDialogModule } from '@angular/material';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AboutComponent } from './about/about.component';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app.routing';
import { DishAddComponent } from './dishes/dish-add/dish-add.component';
import { DishDeleteModalComponent } from './dishes/dish-delete-modal/dish-delete-modal.component';
import { DishDetailComponent } from './dishes/dish-detail/dish-detail.component';
import { DishUpdateComponent } from './dishes/dish-update/dish-update.component';
import { DishesComponent } from './dishes/dishes.component';
import { IngredientAddComponent } from './dishes/ingredients/ingredient-add/ingredient-add.component';
import { IngredientDeleteModalComponent } from './dishes/ingredients/ingredient-delete-modal/ingredient-delete-modal.component';
import { IngredientSingleComponent } from './dishes/ingredients/ingredient-single/ingredient-single.component';
import { IngredientUpdateComponent } from './dishes/ingredients/ingredient-update/ingredient-update.component';
import { IngredientsComponent } from './dishes/ingredients/ingredients.component';
import { IngredientService } from './dishes/ingredients/shared/ingredient.service';
import { DishService } from './dishes/shared/dish.service';
// import { RedirectSilentRenewComponent } from "./redirect-silent-renew/redirect-silent-renew.component";
// import { AddAuthorizationHeaderInterceptor } from "./shared/add-authorization-header-interceptor";
import { EnsureAcceptHeaderInterceptor } from './shared/ensure-accept-header-interceptor';
import { ErrorLoggerService } from './shared/error-logger.service';
import { FooterComponent } from './shared/footer/footer.component';
import { GlobalErrorHandler } from './shared/global-error-handler';
import { HandleHttpErrorInterceptor } from './shared/handle-http-error-interceptor';
// import { OpenIdConnectService } from './shared/open-id-connect.service';
// import { RequireAuthenticatedUserRouteGuardService } from "./shared/require-authenticated-user-route-guard.service";
// import { SigninOidcComponent } from './signin-oidc/signin-oidc.component';

@NgModule({
  declarations: [
    AppComponent,
    DishesComponent,
    AboutComponent,
    IngredientsComponent,
    IngredientAddComponent,
    IngredientSingleComponent,
    DishAddComponent,
    DishDetailComponent,
    DishUpdateComponent,
    IngredientUpdateComponent,
    DishDeleteModalComponent,
    IngredientDeleteModalComponent,
    FooterComponent,
    // SigninOidcComponent,
    // RedirectSilentRenewComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatButtonModule,
    BrowserAnimationsModule,
  ],
  entryComponents: [DishDeleteModalComponent, IngredientDeleteModalComponent],
  providers: [
    // {
    //   provide: HTTP_INTERCEPTORS,
    //   useClass: AddAuthorizationHeaderInterceptor,
    //   multi: true,
    // },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: EnsureAcceptHeaderInterceptor,
      multi: true,
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HandleHttpErrorInterceptor,
      multi: true,
    },
    GlobalErrorHandler,
    ErrorLoggerService,
    DishService,
    IngredientService,
    // OpenIdConnectService,
    // RequireAuthenticatedUserRouteGuardService,
  ],
  bootstrap: [AppComponent],
})
export class AppModule {
  constructor() {}
}
