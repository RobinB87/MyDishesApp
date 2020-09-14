import { RouterReducerState } from '@ngrx/router-store';
import { IDishState, initialDishState } from './dish/dish.state';

export interface IAppState {
  router?: RouterReducerState;
  dishes: IDishState;
}

export const initialAppState: IAppState = {
  dishes: initialDishState,
};

export function getInitialState(): IAppState {
  return initialAppState;
}
