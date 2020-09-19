import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { IAppState } from '../../../core/store';
import { GetDishes, selectDishList } from '../../../core/store/dish';

/* The dish list component */
@Component({
  selector: 'app-dish-list',
  templateUrl: './dish-list.component.html',
  styleUrls: ['./dish-list.component.css'],
})
export class DishListComponent implements OnInit {
  dishes$ = this.store.pipe(select(selectDishList));

  constructor(private store: Store<IAppState>, private router: Router) {}

  ngOnInit() {
    this.store.dispatch(new GetDishes());
  }

  navigateToDish(dishId: number) {
    this.router.navigate(['dish', dishId]);
  }

  // removeDish(dishIndex) {
  //   this.store.dispatch(new DishRemove(dishIndex));
  // }
}
