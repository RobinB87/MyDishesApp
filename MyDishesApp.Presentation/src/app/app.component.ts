import { Component } from '@angular/core';
import { OpenIdConnectService } from './shared/open-id-connect.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'My Dishes App';
  clientHeight: number;

  constructor(private openIdConnectService: OpenIdConnectService) {
    this.clientHeight = window.innerHeight;
  }
}
