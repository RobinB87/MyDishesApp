import { ActionReducerMap } from '@ngrx/store';
import { IAppState } from './app.state';
import { dishReducers } from './dish/dish.reducers';

export const appReducers: ActionReducerMap<IAppState, any> = {
  dishes: dishReducers,
};
