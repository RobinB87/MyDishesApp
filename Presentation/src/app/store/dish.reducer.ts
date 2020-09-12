import { Action, createReducer, on } from '@ngrx/store';
import { Dish } from '../core/models/dish.model';
import * as DishActions from './dish.actions';

export interface DishState {
  dishes: Dish[];
  loading: boolean;
  error: boolean;
}

export const initialState: DishState = {
  dishes: [],
  loading: false,
  error: false,
};

function modifyDishState(
  dishState: DishState,
  dishChanges: Partial<Dish>
): DishState {
  return {
    ...dishState,
    loading: false,
    dishes: dishState.dishes.map((h) => {
      if (h.dishId === dishChanges.dishId) {
        return { ...h, ...dishChanges };
      } else {
        return h;
      }
    }),
  };
}

const dishReducer = createReducer(
  initialState,
  on(DishActions.getDishes, (state) => ({ ...state, loading: true })),
  on(DishActions.getDishesError, (state) => ({ ...state, loading: false })),
  on(DishActions.getDishesSuccess, (state, { dishes }) => ({
    ...state,
    loading: false,
    dishes,
  }))
);

export function reducer(state: DishState | undefined, action: Action) {
  return dishReducer(state, action);
}
