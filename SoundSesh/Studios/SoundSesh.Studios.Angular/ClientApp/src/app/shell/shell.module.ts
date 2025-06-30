import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ShellComponent } from './shell.component';
import { NavbarTopComponent } from './navbar-top/navbar-top.component';
import { FooterComponent } from './footer/footer.component';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { NavbarModule, WavesModule, ButtonsModule } from 'ng-uikit-pro-standard';

@NgModule({
  imports: [
    CommonModule,
    RouterModule,
    NgScrollbarModule,
    NavbarModule,
    WavesModule,
    ButtonsModule
  ],
  declarations: [
    ShellComponent,
    NavbarTopComponent,
    FooterComponent
  ]
})
export class ShellModule { }
