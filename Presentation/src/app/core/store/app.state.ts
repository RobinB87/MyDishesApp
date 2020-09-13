import { IDishState, initialDishState } from './dish/dish.state';

export interface IAppState {
  dishes: IDishState;
}

export const initialAppState: IAppState = {
  dishes: initialDishState,
};

export function getInitialState(): IAppState {
  return initialAppState;
}
