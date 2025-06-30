import { Component, Input, OnInit } from '@angular/core';
import { FileService } from 'src/app/shared/services/file.service';
import { Studio } from 'src/app/shared/models/studio';
import { StudioFileInformation } from '../../studio.file';
import { StudioService } from '../../studio.service';
import { FileInformation } from 'src/app/shared/models/file';
import { BaseComponent } from 'src/app/shared/base/base.component';
import { MDBModalService } from 'ng-uikit-pro-standard';
import { ImageService } from 'src/app/shared/services/image.service';

@Component({
  selector: 'studio-portal-images',
  templateUrl: './images.component.html',
  styleUrls: ['./images.component.css'],
})
export class ImagesComponent extends BaseComponent implements OnInit {

  fileInformation: FileInformation;
  file: File;
  progress: number;
  message: string;
  isDefaultProfileUpload: boolean;
  isDefaultBannerUpload: boolean;
  @Input() studio: Studio;

  constructor(private imageService: ImageService,
              private fileService: FileService,
              private studioService: StudioService, public modalService: MDBModalService) {
                super(modalService);
                this.fileService.fileInformation$.subscribe(fi => this.fileInformation = fi);
                this.fileService.file$.subscribe(file => this.file = file);
  }

  ngOnInit(): void {
    this.isNewImageDefault();
  }

  uploadImage() {
    this.imageService.uploadImage(this.file, this.fileInformation).subscribe((response) => {
      if (response) {
        const sfi = new StudioFileInformation(response, this.studio.id, response.url);

        if (this.fileInformation.subType === 'profile' && this.isDefaultProfileUpload === true) {
          this.studio.applicationfiles.filter(af => af.subType === 'profile').forEach(af => af.isDefault = false);
          sfi.isDefault = this.isDefaultProfileUpload === true;
        }

        if (this.fileInformation.subType === 'banner' && this.isDefaultBannerUpload === true) {
          this.studio.applicationfiles.filter(af => af.subType === 'banner').forEach(af => af.isDefault = false);
          sfi.isDefault = this.isDefaultBannerUpload === true;
        }

        this.studio.applicationfiles.push(sfi);
        this.studioService.update(this.studio).subscribe((su) => {
          this.studio = su;
          this.isNewImageDefault();
        });
      }
    });
  }

  uploadProfileImageAsDefault(event: any) {
    this.isDefaultProfileUpload = event.checked;
  }

  uploadBannerImageAsDefault(event: any) {
    this.isDefaultBannerUpload = event.checked;
  }

  flipImageActivation(id: number) {
    const x = this.studio.applicationfiles.find(af => af.id === id);
    x.isActive = !x.isActive;
    this.studioService.update(this.studio).subscribe((response) => {
      this.studio = response;
      this.isNewImageDefault();
    });
  }

  makeDefault(id: number, subType: string) {
    this.studio.applicationfiles.filter(af => af.subType === subType).forEach(af => af.isDefault = af.id === id);
    this.studioService.update(this.studio).subscribe((response) => {
      this.studio = response;
      this.isNewImageDefault();
    });
  }

  isNewImageDefault() {
    this.isDefaultProfileUpload = (!this.studio.defaultprofileimage || this.studio.defaultprofileimage === null);
    this.isDefaultBannerUpload = (!this.studio.defaultbannerimage || this.studio.defaultbannerimage === null);
  }
}
