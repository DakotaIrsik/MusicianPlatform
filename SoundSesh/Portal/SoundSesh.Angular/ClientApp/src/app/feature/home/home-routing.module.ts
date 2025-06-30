import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { IndexComponent } from './index/index.component';
import { Shell } from 'src/app/shell/shell.service';


const routes: Routes = [
  Shell.childRoutes([
    { path: 'Studios/home', component: IndexComponent },
    { path: 'Musicians/home', component: IndexComponent }
  ])
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  providers: []
})
export class HomeRoutingModule { }
