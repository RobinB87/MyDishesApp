import { createAction, props } from '@ngrx/store';
import { Dish } from '../core/models/dish.model';

export const getDishes = createAction('[Dish] GET_DISHES');

export const getDishesSuccess = createAction(
  '[Dish] GET_DISHES_SUCCESS',
  props<{ dishes: Dish[] }>()
);

export const getDishesError = createAction(
  '[Dish] GET_DISHES_ERROR',
  props<{ error: any }>()
);

export const setDishLoading = createAction(
  '[Dish] SET_DISH_LOADING',
  props<{ loading: boolean }>()
);
