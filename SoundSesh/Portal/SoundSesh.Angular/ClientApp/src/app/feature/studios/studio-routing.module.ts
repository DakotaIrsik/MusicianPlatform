import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Shell } from 'src/app/shell/shell.service';
import { StudioSearchComponent } from './studio-search/studio-search.component';
import { StudioMyStudiosComponent } from './studio-my-studios/studio-my-studios.component';
import { StudioAddComponent } from './studio-add/studio-add.component';
import { StudioEditComponent } from './studio-edit/studio-edit.component';
import { StudioDetailComponent } from './studio-detail/studio-detail.component';
import { AuthCallbackComponent } from '../account/auth-callback/auth-callback.component';
import { AdminComponent } from './studio-administration/admin.component';


const routes: Routes = [
  Shell.childRoutes([
    { path: 'Studios/auth-callback', component: AuthCallbackComponent, /*  canActivate: [AuthGuard] */ },
    { path: 'Studios/administration', component: AdminComponent, /*  canActivate: [AuthGuard] */ },
    { path: 'Studios/search', component: StudioSearchComponent, /*  canActivate: [AuthGuard] */ },
    { path: 'Studios/manage', component: StudioMyStudiosComponent, /*  canActivate: [AuthGuard] */ },
    { path: 'Studios/add', component: StudioAddComponent, /* canActivate: [AuthGuard] */ },
    { path: 'Studios/:id', component: StudioDetailComponent, /* canActivate: [AuthGuard] */ },
    { path: 'Studios/:id/edit', component: StudioEditComponent, /* canActivate: [AuthGuard] */ },
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class StudioRoutingModule { }
