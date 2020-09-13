import { Action } from '@ngrx/store';
import { Dish } from '../../models';

export enum EDishActions {
  GetDishes = '[Dish] Get Dishes',
  GetDishesSuccess = '[Dish] Get Dishes Success',
  Add = '[Dish] Add',
  Remove = '[Dish] Remove',
}

export class ActionEx implements Action {
  readonly type;
  payload: any;
}

export class GetDishes implements Action {
  public readonly type = EDishActions.GetDishes;
}

export class GetDishesSuccess implements Action {
  public readonly type = EDishActions.GetDishesSuccess;
  constructor(public payload: Dish[]) {}
}

export class DishAdd implements ActionEx {
  readonly type = EDishActions.Add;
  constructor(public payload: any) {}
}

export class DishRemove implements ActionEx {
  readonly type = EDishActions.Remove;
  constructor(public payload: any) {}
}

export type DishActions = GetDishes | GetDishesSuccess | DishAdd | DishRemove;
