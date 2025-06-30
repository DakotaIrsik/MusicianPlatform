import { Component, OnInit, } from '@angular/core';
import { Musician } from 'src/app/shared/models/musician';
import { MusicianService } from '../musician.service';
import { Router } from '@angular/router';
import { LookUpSelectionService } from 'src/app/shared/services/lookup-selection.service';


@Component({
  templateUrl: './musician-add.component.html',
  styleUrls: ['./musician-add.component.css'],
})

export class MusicianAddComponent implements OnInit {
  image: File;
  musician: Musician = new Musician();
  message: string;
  progress: number;
  errors: string[] = [];

  constructor(private lookUpSelectionService: LookUpSelectionService,
              private musicianService: MusicianService,
              private router: Router) {
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
  }

  onSubmit() {
    this.musicianService.create(this.musician).subscribe((result) => {
      if (result) {
        this.router.navigate(['/Musicians/home']);
      }
    },
      error => {
        this.errors = error;
      });
  }
}
