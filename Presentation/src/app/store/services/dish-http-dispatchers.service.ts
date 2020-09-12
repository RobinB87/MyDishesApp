import { Injectable } from '@angular/core';
import { Action, Store } from '@ngrx/store';
import * as DishActions from '../dish.actions';
import { DishService } from './dish.service';
import { EntityState } from './entity-state';

/**
 * Make HTTP calls for Dishes
 * dispatch the results (success or error) to ngrx store.
 */
@Injectable()
export class DishHttpDispatchers {
  getDishes() {
    this.dispatchLoading();
    this.dishService.getDishes().subscribe(
      (dishes) => this.dispatch(DishActions.getDishesSuccess({ dishes })),
      (error) => this.dispatch(DishActions.getDishesError(error))
    );
  }

  constructor(
    private store: Store<EntityState>,
    private dishService: DishService
  ) {}

  private dispatch = (action: Action) => this.store.dispatch(action);
  private dispatchLoading = () =>
    this.dispatch(DishActions.setDishLoading({ loading: true }));
}
