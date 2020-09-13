import { Component, Input } from '@angular/core';
import { MasterDetailCommands } from '../../core/master-detail-commands';
import { Dish } from '../../core/models/dish.model';

@Component({
  selector: 'app-dish-list',
  templateUrl: './dish-list.component.html',
  styleUrls: ['./dish-list.component.css'],
})
export class DishListComponent {
  @Input() dishes: Dish[];
  @Input() selectedDish: Dish;
  @Input() commands: MasterDetailCommands<Dish>;

  byId(dish: Dish) {
    return dish.dishId;
  }

  onSelect(dish: Dish) {
    this.commands.select(dish);
  }

  deleteDish(dish: Dish) {
    this.commands.delete(dish);
  }
}
