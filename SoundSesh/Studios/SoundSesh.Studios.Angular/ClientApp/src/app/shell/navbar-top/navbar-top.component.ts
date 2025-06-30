import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { Subscription } from 'rxjs';
import { environment } from 'src/environments/environment';

@Component({
// tslint:disable-next-line: component-selector
  selector: 'studio-portal-navbar-top',
  templateUrl: './navbar-top.component.html',
  styleUrls: ['./navbar-top.component.css']
})
export class NavbarTopComponent implements OnInit, OnDestroy {

  constructor(private authService: AuthService, private spinner: NgxSpinnerService) {
    this.environmentName = environment.environmentName;
    this.production = environment.production;
  }

  name: string;
  isAuthenticated: boolean;
  subscription: Subscription;
  environmentName: string;
  production: boolean;

  ngOnInit() {
    this.subscription = this.authService.authNavStatus$.subscribe(status => this.isAuthenticated = status);
    this.name = this.authService.name;
  }

  login() {
    this.spinner.show();
    this.authService.login();
  }

  signout() {
    this.authService.signout();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
