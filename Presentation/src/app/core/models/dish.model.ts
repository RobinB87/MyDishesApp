import { DishAbstractBase } from './dish-abstract-base.model';
import { Ingredient } from './ingredient.model';

export class Dish extends DishAbstractBase {
  dishId: number;
  ingredients: Ingredient[];
}
