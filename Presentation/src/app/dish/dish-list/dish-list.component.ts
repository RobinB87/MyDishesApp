import { Component } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { Dish } from '../../core/models';
import { DishRemove } from '../../core/store/dish/dish.actions';

@Component({
  selector: 'app-dish-list',
  templateUrl: './dish-list.component.html',
  styleUrls: ['./dish-list.component.css'],
})
export class DishListComponent {
  dishes: Observable<Dish[]>;

  constructor(private store: Store<{ dishes: Dish[] }>) {
    this.dishes = store.pipe(select('dishes'));
  }

  removeDish(dishIndex) {
    this.store.dispatch(new DishRemove(dishIndex));
  }
}
