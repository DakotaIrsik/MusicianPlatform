import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { Shell } from 'src/app/shell/shell.service';
import { StudioSearchComponent } from './studio-search/studio-search.component';
import { StudioMyStudiosComponent } from './studio-my-studios/studio-my-studios.component';
import { StudioAddComponent } from './studio-add/studio-add.component';
import { StudioEditComponent } from './studio-edit/studio-edit.component';
import { StudioDetailComponent } from './studio-detail/studio-detail.component';
import { AuthGuard } from 'src/app/shared/auth.guard';
import { AdminComponent } from './admin/admin.component';


const routes: Routes = [
  Shell.childRoutes([
    { path: 'admin', component: AdminComponent, /* canActivate: [AuthGuard] */  },
    { path: 'studios/search', component: StudioSearchComponent,/*  canActivate: [AuthGuard] */ },
    { path: 'studios/home', component: StudioMyStudiosComponent,/*  canActivate: [AuthGuard] */  },
    { path: 'studios/add', component: StudioAddComponent, /* canActivate: [AuthGuard] */  },
    { path: 'studios/:id', component: StudioDetailComponent, /* canActivate: [AuthGuard] */  },
    { path: 'studios/:id/edit', component: StudioEditComponent, /* canActivate: [AuthGuard] */  },

  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class StudioRoutingModule { }
