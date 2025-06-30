import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthCallbackComponent } from './feature/account/auth-callback/auth-callback.component';
import { IndexComponent } from './feature/home/index/index.component';
import { ShellComponent } from './shell/shell.component';

const routes: Routes = [
/*   { path: 'auth-callback', component: AuthCallbackComponent }, */

  { path: '**', component: ShellComponent }, 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
