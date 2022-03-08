import { CommonInterceptor } from './core/common.interceptor';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import "@angular/compiler";
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { ToDoPreviewComponent } from './features/dashboard/to-do-preview/to-do-preview.component';
import { ToDoCreateEditModule } from './to-do/to-do.module';

import { AuthModule } from '@auth0/auth0-angular';
import { LoginButtonComponent } from './features/login-button/login-button.component';
import { ProfileComponent } from './features/profile/profile.component';

@NgModule({
  declarations: [
    AppComponent,
    DashboardComponent,
    ToDoPreviewComponent,
    LoginButtonComponent,
    ProfileComponent
  ],
  imports: [
    ToDoCreateEditModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    AuthModule.forRoot({
      domain: 'dev-j35kt5s7.us.auth0.com',
      clientId: 'fJeErOLbNFEasA2NM4QMyUvGZDIllFDQ'
    })
  ],
  exports: [DashboardComponent],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: CommonInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
