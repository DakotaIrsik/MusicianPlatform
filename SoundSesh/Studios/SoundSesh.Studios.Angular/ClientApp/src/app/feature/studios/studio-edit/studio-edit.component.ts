import { Component, OnInit } from '@angular/core';
import { StudioService } from '../studio.service';
import { Studio } from 'src/app/shared/models/studio';
import { ActivatedRoute, Router } from '@angular/router';
import { LookUpSelectionService } from 'src/app/shared/services/lookup-selection.service';
import { MDBModalService, TabsetConfig } from 'ng-uikit-pro-standard';
import { BaseComponent } from 'src/app/shared/base/base.component';

@Component({
  selector: 'studio-portal-studio-edit',
  templateUrl: './studio-edit.component.html',
  styleUrls: ['./studio-edit.component.css'],
  providers: [LookUpSelectionService, TabsetConfig]
})
export class StudioEditComponent extends BaseComponent implements OnInit {

  id: number;
  studio: Studio;
  constructor(private route: ActivatedRoute,
              private router: Router,
              private studioService: StudioService, public modalService: MDBModalService) {
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
    });
  }

  delete(id: number) {
    this.studioService.delete(id).subscribe(() => {
      this.router.navigate(['/studios/home']);
    });
  }
}
