// include directives/components commonly used in features modules in this shared modules
// and import me into the feature module
// importing them individually results in: Type xxx is part of the declarations of 2 modules: ...
// Please consider moving to a higher module...
// https://github.com/angular/angular/issues/10646

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatSelectModule, MatOptionModule, MatSlideToggleModule } from '@angular/material';
import { ImageUploadModule } from 'angular2-image-upload';
import { NgxSpinnerModule } from 'ngx-spinner';
import { AutofocusDirective } from './directives/auto-focus.directive';
import { NgxPaginationModule, PaginationControlsComponent, PaginatePipe } from 'ngx-pagination';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FileUploadComponent } from './forms/file-upload/file-upload.component';
import {  CheckboxModule, ButtonsModule, TimePickerModule, CardsFreeModule, CollapseModule, AccordionModule, InputsModule,
          ModalModule, SelectModule, TabsModule, WavesModule} from 'ng-uikit-pro-standard';

          import { ChartsModule } from 'ng2-charts';
import { MdbFileUploadModule} from 'mdb-file-upload';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HoursOfOperationSelectionComponent } from './forms/hours-of-operation-selection/hours-of-operation-selection.component';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { SocialMediaInputComponent } from './forms/social-media-input/social-media-input.component';
import { HoursOfOperationViewComponent } from './forms/hours-of-operation-view/hours-of-operation-view.component';
import { SelectCityComponent } from './forms/select/select-city.component';
import { TextMaskModule } from 'angular2-text-mask';
import { ValidationErrorComponent } from './forms/validation-error/validation-error.component';
import { MinValueValidatorDirective, MaxValueValidatorDirective } from './directives/validation.directives';
import { SpinnerComponent } from './spinners/spinner.component';
import { ToastrModule } from 'ngx-toastr';
import { SharedRoutingModule } from './shared-routing.module';
import { ErrorComponent } from './errors/error.component';
import { PaginationComponent } from './forms/pagination/pagination.component';
import { NgScrollbarModule } from 'ngx-scrollbar';
import { ActionableMetricModalComponent } from './modals/actionable-metric.modal.component';
import { BaseComponent } from './base/base.component';
import { SelectFromApiComponent } from './forms/select/select.component';
import { ChatComponent } from './forms/chat/chat.component';
// https://stackoverflow.com/que./masksirective-doesnt-work-in-a-sub-module
// https://stackoverflow.com/questions/45032043/uncaught-error-unexpected-module-formsmodule-declared-by-the-module-appmodul/45032201

@NgModule({
  imports: [
    CommonModule,
    NgxSpinnerModule,
    NgxPaginationModule,
    MatSlideToggleModule,
    MatSelectModule,
    BrowserAnimationsModule,
    MdbFileUploadModule,
    ImageUploadModule.forRoot(),
    FormsModule,
    ReactiveFormsModule,
    TabsModule,
    WavesModule,
    NgxMaterialTimepickerModule,
    CheckboxModule,
    ButtonsModule,
    TimePickerModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot(),
    SharedRoutingModule,
    NgScrollbarModule,
    ButtonsModule,
    CollapseModule,
    AccordionModule,
    InputsModule,
    ModalModule.forRoot(),
    SelectModule,
    CardsFreeModule,
    ChartsModule
  ],
  declarations: [
    AutofocusDirective,
    FileUploadComponent,
    HoursOfOperationSelectionComponent,
    SelectFromApiComponent,
    SelectCityComponent,
    SocialMediaInputComponent,
    HoursOfOperationViewComponent,
    ValidationErrorComponent,
    MinValueValidatorDirective,
    MaxValueValidatorDirective,
    SpinnerComponent,
    ErrorComponent,
    PaginationComponent,
    ActionableMetricModalComponent,
    BaseComponent,
    ChatComponent,
  ],
  exports: [
    NgxSpinnerModule,
    AutofocusDirective,
    PaginatePipe,
    PaginationControlsComponent,
    MatSelectModule,
    MatOptionModule,
    BrowserAnimationsModule,
    FileUploadComponent,
    SelectFromApiComponent,
    HoursOfOperationSelectionComponent,
    SocialMediaInputComponent,
    MatSlideToggleModule,
    FormsModule,
    ImageUploadModule,
    MdbFileUploadModule,
    WavesModule,
    NgxMaterialTimepickerModule,
    ReactiveFormsModule,
    TimePickerModule,
    HoursOfOperationViewComponent,
    SelectCityComponent,
    CheckboxModule,
    TextMaskModule,
    ValidationErrorComponent,
    MinValueValidatorDirective,
    MaxValueValidatorDirective,
    SpinnerComponent,
    BrowserAnimationsModule,
    ToastrModule,
    PaginationComponent,
    NgScrollbarModule,
    BaseComponent,
    CardsFreeModule,
    TabsModule,
    ChatComponent,
    ChartsModule
  ],
  entryComponents: [
    ActionableMetricModalComponent
  ],
  providers:    []
})
export class SharedModule { }
