import { Component, Input, OnInit } from '@angular/core';
import { Musician } from 'src/app/shared/models/musician';
import { MusicianService } from '../../musician.service';
import { LookUpSelectionService } from 'src/app/shared/services/lookup-selection.service';

@Component({
  selector: 'studio-portal-musician-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class MusicianEditDetailsComponent implements OnInit {

  @Input() musician: Musician = new Musician();

  constructor(private musicianService: MusicianService,
              private lookUpSelectionService: LookUpSelectionService) {
    this.lookUpSelectionService.lookUpSelected$.subscribe(lookUpRelation => {
      const values = lookUpRelation.lookUp.map(lu => lu.value);
      switch (lookUpRelation.key) {
        case 'genre':
          this.musician.genres = values;
          break;
        case 'craft':
          this.musician.crafts = values;
          break;
      }
    });
  }

  ngOnInit(): void {
    this.musicianService.get().subscribe((response) => this.musician = response);
  }

  onSubmit() {
    this.musicianService.update(this.musician).subscribe();
  }
}
