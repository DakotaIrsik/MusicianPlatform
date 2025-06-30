import { Component, Input } from '@angular/core';
import { Studio } from 'src/app/shared/models/studio';
import { Router } from '@angular/router';

@Component({
  selector: 'studio-portal-studio-card',
  templateUrl: './studio-card.component.html',
  styleUrls: ['./studio-card.component.css']
})
export class StudioCardComponent {
  constructor(private router: Router) { }
  @Input() studio: Studio;

  studioClicked() {
    this.router.navigate(['/studios/edit/' + this.studio.id]);
  }
}
