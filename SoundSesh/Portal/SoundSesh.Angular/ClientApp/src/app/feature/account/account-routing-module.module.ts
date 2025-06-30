import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { Shell } from 'src/app/shell/shell.service';
import { ProfileComponent } from './profile/profile.component';


const routes: Routes = [
Shell.childRoutes([
    /* { path: 'accounts/login', component: LoginComponent }, */
    { path: 'Studios/register', component: RegisterComponent },
    { path: 'Musicians/register', component: RegisterComponent },
/*     { path: 'accounts/profile', component: ProfileComponent, /* canActivate: [AuthGuard]   } */
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class AccountRoutingModule { }
