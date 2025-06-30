import { Component, OnInit } from '@angular/core';
import { ApplicationResolver } from 'src/app/shared/base/application.resolver';
import { Studio } from 'src/app/shared/models/studio';
import { Musician } from 'src/app/shared/models/Musician';
import { StudioService } from '../../studios/studio.service';
import { MusicianService } from '../../musicians/musician.service';
import { Router } from '@angular/router';

@Component({
  selector: 'studio-portal-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  application: string;
  studio: Studio;
  musician: Musician;

  constructor(private musicianService: MusicianService,
              private router: Router) {
    this.application = ApplicationResolver.getConfiguration();
  }

  ngOnInit() {
    switch (this.application) {
      case 'Musicians':
        this.musicianService.get().subscribe((response) => {
          if (!response) {
            this.router.navigate(['/add']);
          } else {
            this.router.navigate(['/' + response.id + '/edit']);
          }
        });
    }
  }
}
