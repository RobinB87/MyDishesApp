import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { IAppState } from '../../core/store/app.state';
import { GetDish } from '../../core/store/dish/dish.actions';
import { selectSelectedDish } from '../../core/store/dish/dish.selectors';

/** The dish detail component */
@Component({
  selector: 'app-dish-detail',
  templateUrl: './dish-detail.component.html',
  styleUrls: ['./dish-detail.component.css'],
})
export class DishDetailComponent implements OnInit {
  dish$ = this.store.pipe(select(selectSelectedDish));

  constructor(private store: Store<IAppState>, private route: ActivatedRoute) {}

  ngOnInit() {
    this.store.dispatch(new GetDish(this.route.snapshot.params.dishId));
  }
}
