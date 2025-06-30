import { Component, ViewChild, ElementRef, OnInit, Input  } from '@angular/core';
import { debounceTime, distinctUntilChanged, filter, map } from 'rxjs/operators';
import { fromEvent } from 'rxjs';
import { StudioSearchRequest } from './studio-search-request';
import { Studio } from 'src/app/shared/models/studio';
import { StudioService } from '../studio.service';
import { NgScrollbar } from 'ngx-scrollbar';

@Component({
  selector: 'studio-portal-studio-search',
  templateUrl: './studio-search.component.html',
  styleUrls: ['./studio-search.component.css']
})
export class StudioSearchComponent implements OnInit {
  page = 1;
  studios: Studio[] = [];

  @Input() searchTerm = '';
  @ViewChild(NgScrollbar) searchResultScrollBar: NgScrollbar;
  @ViewChild('smartSearchInput') smartSearchInput: ElementRef;

  constructor(private studioService: StudioService) {
  }

  ngOnInit() {
    fromEvent(this.smartSearchInput.nativeElement, 'keyup').pipe(
      map((event: any) => {
        return event.target.value;
      })
      , filter(res => res.length > 2)
      , debounceTime(1000)
      , distinctUntilChanged()).subscribe((text: string) => {
        this.loadStudios(text);
      });
    this.loadStudios(this.searchTerm);
  }
  loadStudios($searchText) {
    this.studios = [];
    const studioSearchRequest = new StudioSearchRequest();
    studioSearchRequest.state = $searchText;
    studioSearchRequest.size = 1500;
    this.studioService.search(studioSearchRequest).subscribe(
      response => this.studios = response.data
    );
  }

  pageChange($event) {
    this.page = $event;
    this.searchResultScrollBar.smoothScroll.scrollToTop(500);
  }
}
