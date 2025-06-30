import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ShellComponent } from './shell.component';
import { StudioNavbarComponent } from './navbar-top/studio-navbar.component';
import { FooterComponent } from './footer/footer.component';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { NavbarModule, WavesModule, ButtonsModule } from 'ng-uikit-pro-standard';
import { MusicianNavbarComponent } from './navbar-top/musician-navbar.component';

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
    StudioNavbarComponent,
    MusicianNavbarComponent,
    FooterComponent
  ]
})
export class ShellModule { }
