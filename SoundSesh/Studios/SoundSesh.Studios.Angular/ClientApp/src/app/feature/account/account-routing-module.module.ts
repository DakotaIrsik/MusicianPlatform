import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { Shell } from 'src/app/shell/shell.service';
import { AdminComponent } from '../studios/admin/admin.component';


const routes: Routes = [
Shell.childRoutes([
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class AccountRoutingModule { }