import { Component, OnInit, OnDestroy } from '@angular/core';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseNavbar } from './base-navbar';

@Component({
// tslint:disable-next-line: component-selector
  selector: 'portal-musician-navbar',
  templateUrl: './musician-navbar.component.html'
})
export class MusicianNavbarComponent extends BaseNavbar implements OnInit, OnDestroy {

  constructor(authService: AuthService, spinner: NgxSpinnerService) {
    super(authService, spinner);
  }
}
