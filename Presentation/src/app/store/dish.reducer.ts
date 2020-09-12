import { Dish } from '../core/models/dish.model';

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
