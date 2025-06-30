import { Component, OnInit, Input } from '@angular/core';
import { LookUpService } from 'src/app/shared/services/lookup.service';
import { LookUp } from 'src/app/shared/models/api/responses/lookup';
import { Studio } from 'src/app/shared/models/studio';
import { StudioService } from '../../studio.service';
import { SocialMedia } from 'src/app/shared/models/social-media';
import { SocialMediaService } from 'src/app/shared/forms/social-media-input/social-media.service';

@Component({
  selector: 'studio-portal-social-media',
  templateUrl: './social-media.component.html',
  styleUrls: ['./social-media.component.css'],
  providers: [SocialMediaService]
})
export class SocialMediaComponent implements OnInit {

  @Input()
  studio: Studio;
  selections: SocialMedia[] = [];
  socialMedias: LookUp[] = [];


  constructor(private lookUpService: LookUpService,
              private studioService: StudioService,
              private socialMediaService: SocialMediaService) {
    this.socialMediaService.socialMediasChanged$.subscribe(h => {
      this.updateSocialMedia(this.selections, h);
    });
  }

  ngOnInit() {
    this.lookUpService.getSocialMedia().subscribe(response => {
      this.socialMedias = response;
    });
  }

  onSubmit() {
    this.selections.forEach(hoo => {
      this.updateSocialMedia(this.studio.socialmedia, hoo);
    });
    this.studioService.update(this.studio).subscribe();
  }

  getUrl(name: string) {
    const socialMediaPlatform =  this.studio.socialmedia.find(sm => sm.name === name);
    if (socialMediaPlatform) {
      return socialMediaPlatform.url;
    }
    return '';
  }

  updateSocialMedia(arrayToSearch: SocialMedia[], newValue: SocialMedia) {
    const existentHoo = arrayToSearch.find(existing => existing.name === newValue.name);
    if (existentHoo) {
      existentHoo.url = newValue.url;
    } else {
      arrayToSearch.push(newValue);
    }
  }
}
