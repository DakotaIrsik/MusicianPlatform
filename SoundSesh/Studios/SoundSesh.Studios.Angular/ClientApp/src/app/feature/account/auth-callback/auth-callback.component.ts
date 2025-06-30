import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { UserService } from 'src/app/core/user/user.service';

@Component({
  selector: 'studio-portal-auth-callback',
  templateUrl: './auth-callback.component.html',
  styleUrls: ['./auth-callback.component.css']
})
export class AuthCallbackComponent implements OnInit {
  errorMessage: string;
  error: boolean;

  constructor(private authService: AuthService,
              private userService: UserService,
              private router: Router,
              private route: ActivatedRoute) {}

  async ngOnInit() {
    if (this.route.snapshot.fragment.indexOf('error') >= 0) {
       this.error = true;
       return;
     }

    await this.authService.completeAuthentication();

    this.userService.associateIdentityUserToApplication().subscribe(
      response => {
        const result = response;
      },
      error => this.errorMessage = error as any
    );

    this.router.navigate(['/studios/home']);
  }
}
