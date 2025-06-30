import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Shell } from 'src/app/shell/shell.service';
import { MusicianAddComponent } from './musician-add/musician-add.component';
import { MusicianEditComponent } from './musician-edit/musician-edit.component';
import { AuthCallbackComponent } from '../account/auth-callback/auth-callback.component';
import { ProfileComponent } from './profile/profile.component';
import { MusicianComponent } from './musician.component';
import { AdminComponent } from '../studios/studio-administration/admin.component';

const routes: Routes = [
  Shell.childRoutes([
    { path: 'Musicians/auth-callback', component: AuthCallbackComponent, /* canActivate: [AuthGuard] */  },
    { path: 'Musicians/home', component: MusicianComponent, /* canActivate: [AuthGuard] */  },
    { path: 'Musicians/add', component: MusicianAddComponent, /* canActivate: [AuthGuard] */  },
    { path: 'Musicians/:id/edit', component: MusicianEditComponent, /* canActivate: [AuthGuard] */  },
    { path: 'Musicians/profile', component: ProfileComponent, /* canActivate: [AuthGuard] */  },
    { path: 'Musicians/administration', component: AdminComponent, /*  canActivate: [AuthGuard] */ },
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class MusicianRoutingModule { }
