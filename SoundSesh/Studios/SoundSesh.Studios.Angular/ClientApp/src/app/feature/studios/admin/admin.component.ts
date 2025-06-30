import { Component, OnInit } from '@angular/core';
import { TabsetConfig } from 'ng-uikit-pro-standard';

@Component({
  selector: 'studio-portal-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss'],
  providers: [TabsetConfig]
})
export class AdminComponent implements OnInit {

  constructor() { }

  ngOnInit() {
  }

}
