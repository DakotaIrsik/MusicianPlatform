import { Component, OnInit, Input, } from '@angular/core';
import { Musician } from 'src/app/shared/models/musician';
import { Router } from '@angular/router';
import { LookUpSelectionService } from 'src/app/shared/services/lookup-selection.service';
import { MusicianService } from '../../musician.service';
import { LookUpService } from 'src/app/shared/services/lookup.service';
import { LookUp } from 'src/app/shared/models/api/responses/lookup';


@Component({
  selector: 'studio-portal-musician-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css'],
})

export class MusicianEditProfileComponent implements OnInit {
  image: File;
  @Input() musician: Musician = new Musician();
  message: string;
  progress: number;
  errors: string[] = [];
  socialMedias: LookUp[] = [];

  constructor(private lookUpSelectionService: LookUpSelectionService,
              private musicianService: MusicianService,
              private lookUpService: LookUpService) {
    this.lookUpSelectionService.lookUpSelected$.subscribe(lookUpRelation => {
      const values = lookUpRelation.lookUp.map(lu => lu.value);
      switch (lookUpRelation.key) {
        case 'state':
          this.musician.state =  values[0];
          break;
        case 'city':
          this.musician.city = values[0];
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
