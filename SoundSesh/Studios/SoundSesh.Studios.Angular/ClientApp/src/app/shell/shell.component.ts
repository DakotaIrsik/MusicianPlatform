import { Component, OnInit, ViewChild } from '@angular/core';
import { NgScrollbar } from 'ngx-scrollbar';
import { Router, NavigationEnd } from '@angular/router';
import { filter, tap } from 'rxjs/operators';

@Component({
  selector: 'studio-portal-app-shell',
  templateUrl: './shell.component.html',
  styleUrls: ['./shell.component.css']
})
export class ShellComponent implements OnInit {
  @ViewChild(NgScrollbar) shellScrollbar: NgScrollbar;

  constructor(router: Router) {
    router.events.pipe(
      filter(event => event instanceof NavigationEnd)),
      filter(() => !!this.shellScrollbar),
      tap((event: NavigationEnd) => this.shellScrollbar.scrollToTop());
  }

  ngOnInit() {
  }

}
