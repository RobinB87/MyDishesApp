import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { EffectsModule } from '@ngrx/effects';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from '../environments/environment';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LogInComponent } from './auth/log-in/log-in.component';
import { SignUpComponent } from './auth/sign-up/sign-up.component';
import { DishService } from './core/services/dish.service';
import { appReducers } from './core/store/app.reducers';
import { DishEffects } from './core/store/dish/dish.effects';
import { DishAddComponent } from './dish/dish-add';
import { DishDetailComponent } from './dish/dish-detail/dish-detail.component';
import { DishListComponent } from './dish/dish-list';

@NgModule({
  declarations: [
    AppComponent,
    DishListComponent,
    DishAddComponent,
    DishDetailComponent,
    SignUpComponent,
    LogInComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    StoreModule.forRoot(appReducers),
    EffectsModule.forRoot([DishEffects]),
    StoreRouterConnectingModule.forRoot({ stateKey: 'router' }),
    !environment.production ? StoreDevtoolsModule.instrument() : [],
    AppRoutingModule,
  ],
  providers: [DishService],
  bootstrap: [AppComponent],
})
export class AppModule {}
