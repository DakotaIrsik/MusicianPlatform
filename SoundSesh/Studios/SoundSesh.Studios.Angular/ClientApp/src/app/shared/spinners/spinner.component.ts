import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'studio-portal-spinner',
  templateUrl: './spinner.component.html'
})
export class SpinnerComponent {
  constructor() { }

  @Input() spinnerName = '';
  @Input() text = '';
}
