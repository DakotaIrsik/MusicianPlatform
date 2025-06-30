import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/core/authentication/auth.service';

@Component({
  selector: 'studio-portal-diagnostics',
  templateUrl: './diagnostics.component.html',
  styleUrls: ['./diagnostics.component.css']
})
export class DiagnosticsComponent implements OnInit {
  userId: string;
  token: string;
  userName: string;
  isLoggedin: boolean;


  constructor(private authService: AuthService) { }


  ngOnInit() {
    this.userId = this.authService.sub;
    this.userName = this.authService.name;
    this.token = this.authService.authorizationHeaderValue;
    this.isLoggedin = this.authService.isAuthenticated();
  }
}
