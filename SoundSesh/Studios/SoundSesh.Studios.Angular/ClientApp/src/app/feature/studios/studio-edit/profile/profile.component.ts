import { Component, Input } from '@angular/core';
import { Studio } from 'src/app/shared/models/studio';
import { StudioService } from '../../studio.service';
import { LookUpSelectionService } from 'src/app/shared/services/lookup-selection.service';

@Component({
  selector: 'studio-portal-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent {

  @Input()
  studio: Studio;
  error: string;
  constructor(private studioService: StudioService,
              private lookUpSelectionService: LookUpSelectionService) {
    this.lookUpSelectionService.lookUpSelected$.subscribe(lookUpRelation => {
      const values = lookUpRelation.lookUp.map(lu => lu.value);
      switch (lookUpRelation.key) {
        case 'state':
          this.studio.state = values[0];
          break;
        case 'city':
          this.studio.city = values[0];
      }
    });
  }

  onSubmit() {
    this.studioService.update(this.studio).subscribe();
  }
}

