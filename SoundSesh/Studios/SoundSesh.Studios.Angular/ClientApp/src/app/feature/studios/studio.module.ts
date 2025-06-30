import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StudioSearchComponent } from './studio-search/studio-search.component';
import { StudioService } from './studio.service';
import { StudioRoutingModule } from './studio-routing.module';
import { SharedModule } from 'src/app/shared/shared.module';
import { StudioMyStudiosComponent } from './studio-my-studios/studio-my-studios.component';
import { StudioAddComponent } from './studio-add/studio-add.component';
import { StudioEditComponent } from './studio-edit/studio-edit.component';
import { StudioCardComponent } from './studio-card/studio-card.component';
import { StudioDetailComponent } from './studio-detail/studio-detail.component';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { ProfileComponent } from './studio-edit/profile/profile.component';
import { HoursComponent } from './studio-edit/hours/hours.component';
import { ImagesComponent } from './studio-edit/images/images.component';
import { DetailsComponent } from './studio-edit/details/details.component';
import { SocialMediaComponent } from './studio-edit/social-media/social-media.component';
import { RoomsComponent } from './studio-edit/rooms/rooms.component';
import { WeblinksComponent } from './studio-edit/weblinks/weblinks.component';
import { AuthGuard } from 'src/app/shared/auth.guard';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { SpinnerInterceptor } from 'src/app/shared/interceptors/spinner-interceptor';
import { ToasterInterceptor } from 'src/app/shared/interceptors/toaster-interceptor';
import { AdminComponent } from './admin/admin.component';
import { DiagnosticsComponent } from './admin/diagnostics/diagnostics.component';
import { FeatureRequestsComponent } from './admin/feature-requests/feature-requests.component';
import { TabulatorComponent } from 'src/app/shared/tabulator/tabulator.component';
import { ChartsModule } from 'ng-uikit-pro-standard';

@NgModule({
  declarations: [
    StudioSearchComponent,
    StudioMyStudiosComponent,
    StudioAddComponent,
    StudioEditComponent,
    StudioCardComponent,
    StudioDetailComponent,
    ProfileComponent,
    HoursComponent,
    ImagesComponent,
    DetailsComponent,
    SocialMediaComponent,
    RoomsComponent,
    WeblinksComponent,
    AdminComponent,
    DiagnosticsComponent,
    FeatureRequestsComponent,
    TabulatorComponent
  ],
  providers: [
    AuthService,
    StudioService,
    AuthGuard,
    { provide: HTTP_INTERCEPTORS, useClass: SpinnerInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ToasterInterceptor, multi: true },
  ],
  imports: [
    CommonModule,
    StudioRoutingModule,
    SharedModule,
    ChartsModule
  ],

})

export class StudioModule { }
