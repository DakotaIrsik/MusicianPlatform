import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MusicianService } from './musician.service';
import { SharedModule } from 'src/app/shared/shared.module';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { AuthGuard } from 'src/app/shared/auth.guard';
import { ChartsModule } from 'ng-uikit-pro-standard';
import { MusicianAddComponent } from './musician-add/musician-add.component';
import { MusicianEditComponent } from './musician-edit/musician-edit.component';
import { MusicianEditDetailsComponent } from './musician-edit/details/details.component';
import { ImagesComponent } from './musician-edit/images/images.component';
import { WeblinksComponent } from './musician-edit/weblinks/weblinks.component';
import { MusicianRoutingModule } from './musician-routing.module';
import { MusicianEditProfileComponent } from './musician-edit/profile/profile.component';
import { ProfileComponent } from './profile/profile.component';
import { MusicianComponent } from './musician.component';
import { AdminComponent } from './musician-administration/admin.component';
import { DiagnosticsComponent } from './musician-administration/diagnostics/diagnostics.component';
import { FeatureRequestsComponent } from './musician-administration/feature-requests/feature-requests.component';


@NgModule({
  imports: [
    CommonModule,
    MusicianRoutingModule,
    SharedModule,
    ChartsModule
  ],
  declarations: [
    MusicianAddComponent,
    MusicianEditComponent,
    MusicianEditDetailsComponent,
    MusicianEditProfileComponent,
    ImagesComponent,
    WeblinksComponent,
    ProfileComponent,
    MusicianComponent,
    AdminComponent,
    DiagnosticsComponent,
    FeatureRequestsComponent
  ],
  providers: [
    AuthService,
    MusicianService,
    AuthGuard
  ]
})

export class MusicianModule { }
