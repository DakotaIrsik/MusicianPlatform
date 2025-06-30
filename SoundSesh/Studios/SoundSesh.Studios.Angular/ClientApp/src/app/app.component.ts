import { Component, ViewChild } from '@angular/core';
import { NgScrollbar } from 'ngx-scrollbar';
import { Router,  } from '@angular/router';
@Component({
  selector: 'studio-portal-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'StudioPortal';
  
 @ViewChild(NgScrollbar) appScrollbar: NgScrollbar;

  constructor(router: Router) {
/*     router.events.pipe(
      filter(event => event instanceof NavigationEnd)),
      filter(() => !!this.appScrollbar),
      tap((event: NavigationEnd) => this.appScrollbar.scrollToTop()); */
  }
}
