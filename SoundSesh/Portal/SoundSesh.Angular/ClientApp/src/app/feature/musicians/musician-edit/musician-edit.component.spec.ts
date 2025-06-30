import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MusicianEditComponent } from './musician-edit.component';

describe('MusicianEditComponent', () => {
  let component: MusicianEditComponent;
  let fixture: ComponentFixture<MusicianEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MusicianEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MusicianEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
