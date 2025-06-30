import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ApplicationResolver } from 'src/app/shared/base/application.resolver';

@Component({
  selector: 'portal-musician',
  templateUrl: './musician.component.html',
})
export class MusicianComponent implements OnInit {

  environment: any;
  application: string;

  constructor() {
    this.environment = environment;
    this.application = ApplicationResolver.getConfiguration();
  }
  ngOnInit() {
  }

}
