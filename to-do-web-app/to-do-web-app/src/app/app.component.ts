import { AuthModule, AuthService } from '@auth0/auth0-angular';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'to-do-web-app';

  constructor(public auth: AuthService){}
}
