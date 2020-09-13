import { Dish } from '../../models';

export interface IDishState {
  dishes: Dish[];
  selectedDish: Dish[];
}

export const initialDishState: IDishState = {
  dishes: null,
  selectedDish: null,
};
