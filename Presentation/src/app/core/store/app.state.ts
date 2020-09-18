import { RouterReducerState } from '@ngrx/router-store';
import * as auth from './auth/auth.reducers';
import { IDishState, initialDishState } from './dish/dish.state';

export interface IAppState {
  router?: RouterReducerState;
  dishes: IDishState;
  authState: auth.State;
}

export const initialAppState: IAppState = {
  dishes: initialDishState,
};

export function getInitialState(): IAppState {
  return initialAppState;
}
