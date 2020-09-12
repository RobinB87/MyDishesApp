import { ActionReducerMap } from '@ngrx/store';
import * as fromDishes from '../dish.reducer';

export interface EntityState {
  dishes: fromDishes.DishState;
}

export const reducers: ActionReducerMap<EntityState> = {
  dishes: fromDishes.reducer,
};
