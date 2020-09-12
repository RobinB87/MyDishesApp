import { Injectable } from '@angular/core';
import { createFeatureSelector, createSelector, Store } from '@ngrx/store';
import { DishState } from '../dish.reducer';
import { EntityState } from './entity-state';

// selectors
const getEntityState = createFeatureSelector<EntityState>('entityCache');

const getDishState = createSelector(
  getEntityState,
  (state: EntityState) => state.dishes
);

const getAllDishes = createSelector(
  getDishState,
  (state: DishState) => state.dishes
);

const getDishesLoading = createSelector(
  getDishState,
  (state: DishState) => state.loading
);

@Injectable()
export class DishSelectors {
  constructor(private store: Store<EntityState>) {}
  // selectors$
  dishes$ = this.store.select(getAllDishes);
  dishState$ = this.store.select(getDishState);
  loading$ = this.store.select(getDishesLoading);
}
