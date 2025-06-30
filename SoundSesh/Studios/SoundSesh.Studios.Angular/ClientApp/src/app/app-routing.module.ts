import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthCallbackComponent } from './feature/account/auth-callback/auth-callback.component';
import { AdminComponent } from './feature/studios/admin/admin.component';

const routes: Routes = [

  { path: 'auth-callback', component: AuthCallbackComponent },
  { path: '**', redirectTo: '', pathMatch: 'full' },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
