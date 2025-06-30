import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SharedModule } from './shared/shared.module';
import { StudioModule } from './feature/studios/studio.module';
import { ShellModule } from './shell/shell.module';
import { HomeModule } from './feature/home/home.module';
import { AccountModule } from './feature/account/account.module';
import { AuthenticationInterceptor } from './shared/interceptors/authentication-interceptor';
import { AuthCallbackComponent } from './feature/account/auth-callback/auth-callback.component';
import { ServerErrorInterceptor } from './shared/interceptors/servererror-interceptor';

@NgModule({
  declarations: [
    AppComponent,
    AuthCallbackComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    HomeModule,
    AccountModule,
    StudioModule,
    AppRoutingModule,
    ShellModule,
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ServerErrorInterceptor, multi: true },
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule {}
