import { Component, Input, OnInit } from '@angular/core';
import { Masks } from 'src/app/shared/masks';
import { MusicianService } from '../../musician.service';
import { Musician } from 'src/app/shared/models/musician';
import { LookUpService } from 'src/app/shared/services/lookup.service';
import { LookUp } from 'src/app/shared/models/api/responses/lookup';
import { SocialMedia } from 'src/app/shared/models/social-media';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'studio-portal-weblinks',
  templateUrl: './weblinks.component.html',
  styleUrls: ['./weblinks.component.css']
})
export class WeblinksComponent extends Masks implements OnInit {
  public myForm: FormGroup;
  @Input() musician: Musician;
  socialMedias: LookUp[] = [];
  staticUrls: SocialMedia[] = [];
  dynamicUrls: SocialMedia[] = [];
  others: string[] = [];
  constructor(private musicianService: MusicianService,
              private lookUpService: LookUpService) {
    super();
  }

  ngOnInit(): void {
    this.lookUpService.getSocialMedia().subscribe(response => {
      this.socialMedias = response;
    });
    this.myForm.valueChanges.subscribe(data => this.onStaticUrlChange(data));
  }

  trackByFn(index: any, item: any) {
    return index;
  }

  onCustomUrlAdded(event: SocialMedia[]) {
    this.dynamicUrls = event;
  }

  onStaticUrlChange(event: SocialMedia[]) {
    this.dynamicUrls = event;
  }

  onSubmit(event: any) {
    this.musicianService.update(this.musician).subscribe();
  }
}
