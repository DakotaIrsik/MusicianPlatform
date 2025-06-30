import { Component, OnInit } from '@angular/core';
import { MusicianService } from '../musician.service';
import { MDBModalService, TabsetConfig } from 'ng-uikit-pro-standard';
import { LookUpSelectionService } from 'src/app/shared/services/lookup-selection.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Musician } from 'src/app/shared/models/musician';
import { BaseComponent } from 'src/app/shared/base/base.component';

@Component({
  selector: 'studio-portal-musician-edit',
  templateUrl: './musician-edit.component.html',
  styleUrls: ['./musician-edit.component.scss'],
  providers: [ MusicianService, LookUpSelectionService, TabsetConfig]
})
export class MusicianEditComponent extends BaseComponent implements OnInit {


  id: number;
  musician: Musician;
  constructor(private route: ActivatedRoute,
              private router: Router,
              private musicianService: MusicianService,
              public modalService: MDBModalService) {
                super(modalService);
              }

  ngOnInit() {
    this.route.paramMap.subscribe(
      params => {
        this.id = +params.get('id');
        this.musicianService.get().subscribe(data => {
          this.musician = data;
        });
      }
    );
  }

}
