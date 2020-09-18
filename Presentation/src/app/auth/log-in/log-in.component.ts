import { Component } from '@angular/core';
import { User } from '../../core/models/authentication/user.model';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css'],
})
export class LogInComponent {
  user: User = new User();

  onSubmit(): void {
    console.log(this.user);
  }
}
