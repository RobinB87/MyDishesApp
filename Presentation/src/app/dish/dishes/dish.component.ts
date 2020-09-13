import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Dish } from '../../core/models/dish.model';
import { DishDispatchers } from '../../store/services/dish.dispatchers';
import { DishSelectors } from '../../store/services/dish.selectors';

@Component({
  selector: 'app-dishes',
  templateUrl: './dish.component.html',
  styleUrls: ['./dish.component.css'],
})
export class DishComponent implements OnInit {
  selected: Dish;

  dishes$: Observable<Dish[]>;
  loading$: Observable<boolean>;

  constructor(
    private dishDispatchers: DishDispatchers,
    private dishSelectors: DishSelectors
  ) {
    this.dishes$ = this.dishSelectors.dishes$;
    this.loading$ = this.dishSelectors.loading$;
  }

  ngOnInit() {
    this.getDishes();
  }

  close() {
    this.selected = null;
  }

  getDishes() {
    this.close();
    this.dishDispatchers.getDishes();
  }
}
