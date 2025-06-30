import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { StudioCardDecks, Studio } from 'src/app/shared/models/studio';
import { StudioService } from '../studio.service';
import { StudioSearchRequest } from '../studio-search/studio-search-request';

@Component({
  selector: 'studio-portal-my-studios',
  templateUrl: './studio-my-studios.component.html',
  styleUrls: ['./studio-my-studios.component.css']
})
export class StudioMyStudiosComponent implements OnInit {
  page = 1;
  studios: Studio[] = [];
  hasStudios?: boolean = null;

  constructor(private studioService: StudioService, private router: Router, private route: ActivatedRoute) {
   }

  ngOnInit() {
    this.loadMyStudios();
  }
  loadMyStudios() {
    this.studios = [];
    const studioSearchRequest = new StudioSearchRequest();
    studioSearchRequest.size = 1500;
    this.studioService.mystudios(studioSearchRequest).subscribe(
      response => this.studios = response.data
    );
  }

  pageChange($event) {
    this.page = $event;
  }

}
