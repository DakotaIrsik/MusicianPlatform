import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { finalize } from 'rxjs/operators';
import { UserRegistration } from 'src/app/shared/models/user-registration';
import { AuthService } from 'src/app/core/authentication/auth.service';


@Component({
  selector: 'studio-portal-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  success: boolean;
  error: string;
  userRegistration: UserRegistration = new UserRegistration();
  submitted = false;

  constructor(private authService: AuthService,
              private spinner: NgxSpinnerService) {
  }
errorMessage: string;
  ngOnInit() {
  }

  onSubmit() {
    this.spinner.show();
    this.authService.register(this.userRegistration)
      .pipe(finalize(() => {
        this.spinner.hide();
      }))
      .subscribe(
      result => {
         if (result) {
           this.success = true;
           this.authService.login();
         }
      },
      error => {
        this.error = error;
      });
  }
}
