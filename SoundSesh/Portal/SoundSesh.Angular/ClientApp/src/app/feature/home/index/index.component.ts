import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ApplicationResolver } from 'src/app/shared/base/application.resolver';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'studio-portal-app-index',
  templateUrl: './index.component.html',
  styleUrls: ['./index.component.css']
})
export class IndexComponent implements OnInit {
  environment: any;
  application: string;

  constructor() {
    this.environment = environment;
    this.application = ApplicationResolver.getConfiguration();
  }

  ngOnInit() {
  }

}
