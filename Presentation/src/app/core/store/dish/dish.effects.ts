import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import { Store } from '@ngrx/store';
import { of } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { Dish } from '../../models';
import { DishService } from '../../services/dish.service';
import { IAppState } from '../app.state';
import { EDishActions, GetDishes, GetDishesSuccess } from './dish.actions';

@Injectable()
export class DishEffects {
  @Effect()
  getDishes$ = this.actions$.pipe(
    ofType<GetDishes>(EDishActions.GetDishes),
    switchMap(() => this.dishService.getDishes()),

    switchMap((dishes: Dish[]) => of(new GetDishesSuccess(dishes)))

    // switchMap((dishHttp: IDishHttp) =>
    //   of(new GetDishesSuccess(dishHttp.dishes))
    // )
  );

  constructor(
    private dishService: DishService,
    private actions$: Actions,
    private store: Store<IAppState>
  ) {}
}
