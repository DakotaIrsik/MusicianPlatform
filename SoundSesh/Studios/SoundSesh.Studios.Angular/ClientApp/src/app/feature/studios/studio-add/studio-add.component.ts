import { Component, OnInit, } from '@angular/core';
import { Studio } from 'src/app/shared/models/studio';
import { StudioService } from '../studio.service';
import { Router } from '@angular/router';
import { LookUpSelectionService } from 'src/app/shared/services/lookup-selection.service';


@Component({
  templateUrl: './studio-add.component.html',
  styleUrls: ['./studio-add.component.css'],
})

export class StudioAddComponent implements OnInit {
  image: File;
  studio: Studio = new Studio();
  message: string;
  progress: number;
  errors: string[] = [];

  constructor(private lookUpSelectionService: LookUpSelectionService,
              private studioService: StudioService,
              private router: Router) {
    this.lookUpSelectionService.lookUpSelected$.subscribe(lookUpRelation => {
      const values = lookUpRelation.lookUp.map(lu => lu.value);
      switch (lookUpRelation.key) {
        case 'state':
          this.studio.state =  values[0];
          break;
        case 'city':
          this.studio.city = values[0];
          break;
        case 'skilllevel':
          this.studio.skilllevel = values[0];
          break;
      }
    });

  }

  ngOnInit(): void {
  }

  onSubmit() {
    this.studioService.create(this.studio).subscribe((result) => {
      if (result) {
        this.router.navigate(['/studios/home']);
      }
    },
      error => {
        this.errors = error;
      });
  }
}
