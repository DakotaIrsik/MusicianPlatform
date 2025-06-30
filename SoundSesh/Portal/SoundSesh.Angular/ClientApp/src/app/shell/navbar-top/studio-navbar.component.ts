import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseNavbar } from './base-navbar';

@Component({
// tslint:disable-next-line: component-selector
  selector: 'portal-studio-navbar',
  templateUrl: './studio-navbar.component.html',
})
export class StudioNavbarComponent extends BaseNavbar implements OnInit, OnDestroy {
  constructor(authService: AuthService, spinner: NgxSpinnerService) {
    super(authService, spinner);
  }
}
