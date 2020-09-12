import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import * as DishActions from './dish.actions';
import { DishService } from './services/dish.service';

@Injectable()
export class DishEffects {
  getDishes$ = createEffect(() =>
    this.actions$.pipe(
      ofType(DishActions.getDishes),
      switchMap(() =>
        this.dishService.getDishes().pipe(
          map((dishes) => DishActions.getDishesSuccess({ dishes })),
          catchError((error) => of(DishActions.getDishesError({ error })))
        )
      )
    )
  );

  constructor(private actions$: Actions, private dishService: DishService) {}
}
