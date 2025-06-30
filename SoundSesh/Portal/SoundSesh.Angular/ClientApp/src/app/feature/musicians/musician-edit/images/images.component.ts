import { Component, Input, OnInit } from '@angular/core';
import { MusicianImageService } from 'src/app/feature/musicians/musician-image.service';
import { FileService } from 'src/app/shared/services/file.service';
import { Musician } from 'src/app/shared/models/musician';
import { MusicianFileInformation } from '../../musician.file';
import { MusicianService } from '../../musician.service';
import { FileInformation } from 'src/app/shared/models/file';
import { BaseComponent } from 'src/app/shared/base/base.component';
import { MDBModalService } from 'ng-uikit-pro-standard';

@Component({
  selector: 'musician-portal-images',
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
  @Input() musician: Musician;

  constructor(private imageService: MusicianImageService,
              private fileService: FileService,
              private musicianService: MusicianService, public modalService: MDBModalService) {
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
        const sfi = new MusicianFileInformation(response, this.musician.id, response.url);

        if (this.fileInformation.subType === 'profile' && this.isDefaultProfileUpload === true) {
          this.musician.applicationfiles.filter(af => af.subType === 'profile').forEach(af => af.isDefault = false);
          sfi.isDefault = this.isDefaultProfileUpload === true;
        }

        if (this.fileInformation.subType === 'banner' && this.isDefaultBannerUpload === true) {
          this.musician.applicationfiles.filter(af => af.subType === 'banner').forEach(af => af.isDefault = false);
          sfi.isDefault = this.isDefaultBannerUpload === true;
        }

        this.musician.applicationfiles.push(sfi);
        this.musicianService.update(this.musician).subscribe((su) => {
          this.musician = su;
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
    const x = this.musician.applicationfiles.find(af => af.id === id);
    x.isActive = !x.isActive;
    this.musicianService.update(this.musician).subscribe((response) => {
      this.musician = response;
      this.isNewImageDefault();
    });
  }

  makeDefault(id: number, subType: string) {
    this.musician.applicationfiles.filter(af => af.subType === subType).forEach(af => af.isDefault = af.id === id);
    this.musicianService.update(this.musician).subscribe((response) => {
      this.musician = response;
      this.isNewImageDefault();
    });
  }

  isNewImageDefault() {
    this.isDefaultProfileUpload = (!this.musician.defaultprofileimage || this.musician.defaultprofileimage === null);
    this.isDefaultBannerUpload = (!this.musician.defaultbannerimage || this.musician.defaultbannerimage === null);
  }
}
