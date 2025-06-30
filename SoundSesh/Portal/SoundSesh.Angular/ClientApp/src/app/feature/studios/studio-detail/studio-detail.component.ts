import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StudioService } from '../studio.service';
import { Studio } from 'src/app/shared/models/studio';
import { AuthService } from 'src/app/core/authentication/auth.service';
import { MDBModalService } from 'ng-uikit-pro-standard';
import { BaseComponent } from 'src/app/shared/base/base.component';

@Component({
  selector: 'studio-portal-studio-detail',
  templateUrl: './studio-detail.component.html',
  styleUrls: ['./studio-detail.component.css'],
})
export class StudioDetailComponent extends BaseComponent implements OnInit {

  private id: number;
  public studio: Studio;
  public editable: boolean;

  constructor(private route: ActivatedRoute,
              private studioService: StudioService,
              private authService: AuthService,
              public modalService: MDBModalService) {
    super(modalService);
  }

  ngOnInit() {
    this.route.paramMap.subscribe(
      params => {
        this.id = +params.get('id');
        this.getStudioDetail(this.id);
      }
    );
  }

  getStudioDetail(id: number) {
    this.studioService.get(id).subscribe(data => {
      this.studio = data;
      this.editable = data.userid === this.authService.sub;
    });
  }
  writeReview() {

  }
}
