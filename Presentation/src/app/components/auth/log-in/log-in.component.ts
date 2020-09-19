import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { User } from '../../../core/models/auth';
import { IAppState } from '../../../core/store';
import { LogIn } from '../../../core/store/auth';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css'],
})
export class LogInComponent implements OnInit {
  user: User = new User();

  constructor(private store: Store<IAppState>) {}

  ngOnInit() {}

  onSubmit(): void {
    const payload = {
      email: this.user.email,
      password: this.user.password,
    };
    this.store.dispatch(new LogIn(payload));
  }
}
