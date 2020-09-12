import { Injectable } from '@angular/core';
import { Store } from '@ngrx/store';
import * as DishAction from '../dish.actions';
import { EntityState } from './entity-state';

@Injectable()
export class DishDispatchers {
  constructor(private store: Store<EntityState>) {}

  getDishes() {
    this.store.dispatch(DishAction.getDishes());
  }
}
