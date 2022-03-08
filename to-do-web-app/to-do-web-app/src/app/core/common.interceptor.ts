import { AuthService } from '@auth0/auth0-angular';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Auth0Client } from '@auth0/auth0-spa-js';

@Injectable()
export class CommonInterceptor implements HttpInterceptor {
  accessToken!: string;

  constructor(private auth: Auth0Client) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.auth.getTokenSilently(
      (token: string) => this.accessToken = token
    );
    console.log(this.accessToken);

    const cloned = request.clone(
      {
        headers: request.headers.set("Authorization", this.accessToken)
      }
    );
    return next.handle(cloned);
  }
}
