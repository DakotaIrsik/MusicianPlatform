import { Component, Input, OnInit } from '@angular/core';
import { StudioService } from '../../studio.service';
import { Studio } from 'src/app/shared/models/studio';
import { Masks } from 'src/app/shared/masks';

@Component({
  selector: 'studio-portal-weblinks',
  templateUrl: './weblinks.component.html',
  styleUrls: ['./weblinks.component.css']
})
export class WeblinksComponent extends Masks implements OnInit {
  @Input() studio: Studio;
  formattedWebsiteLink: string;
  formattedAudioClipLink: string;

  constructor(private studioService: StudioService) {
    super();
   }

  ngOnInit(): void {
    this.formattedWebsiteLink = (this.studio.websitelink) ? this.studio.websitelink.replace('https://', '') : '';
    this.formattedAudioClipLink = (this.studio.audiocliplink) ? this.studio.audiocliplink.replace('https://', '') : '';
  }

  onSubmit() {
    this.studio.websitelink = 'https://' + this.formattedWebsiteLink;
    this.studio.audiocliplink = 'https://' + this.formattedAudioClipLink;
    this.studioService.update(this.studio).subscribe();
  }
}
