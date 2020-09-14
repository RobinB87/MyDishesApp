import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { DishService } from './core/services/dish.service';
import { appReducers } from './core/store/app.reducers';
import { DishEffects } from './core/store/dish/dish.effects';
import { DishAddComponent } from './dish/dish-add';
import { DishListComponent } from './dish/dish-list';

@NgModule({
  declarations: [AppComponent, DishListComponent, DishAddComponent],
  imports: [
    AppRoutingModule,
    BrowserModule,
    HttpClientModule,
    StoreModule.forRoot(appReducers),
    EffectsModule.forRoot([DishEffects]),
  ],
  providers: [DishService],
  bootstrap: [AppComponent],
})
export class AppModule {}
