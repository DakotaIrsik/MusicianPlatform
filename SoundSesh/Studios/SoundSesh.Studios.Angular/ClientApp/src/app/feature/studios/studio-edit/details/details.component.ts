import { Component, Input } from '@angular/core';
import { Studio } from 'src/app/shared/models/studio';
import { StudioService } from '../../studio.service';
import { LookUpSelectionService } from 'src/app/shared/services/lookup-selection.service';

@Component({
  selector: 'studio-portal-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent {
  @Input() studio: Studio;

  constructor(private studioService: StudioService,
              private lookUpSelectionService: LookUpSelectionService) {

    this.lookUpSelectionService.lookUpSelected$.subscribe(lookUpRelation => {
      const values = lookUpRelation.lookUp.map(lu => lu.value);
      switch (lookUpRelation.key) {
        case 'genre':
          this.studio.genres = values;
          break;
        case 'amenity':
          this.studio.amenities = values;
          break;
        case 'skilllevel':
          this.studio.skilllevel = values[0];
          break;
      }
    });
  }

  onSubmit() {
    this.studioService.update(this.studio).subscribe();
  }
}
