import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable()
export class StudioEditService {

  // Observable string sources
  private stateSource = new Subject<string>();
  private citySource = new Subject<string>();
  private skillLevel = new Subject<string>();
  private imageSource = new Subject<File>();


  // Observable string streams
  stateSelected$ = this.stateSource.asObservable();
  citySelected$ = this.citySource.asObservable();
  skillLevelSelected$ = this.skillLevel.asObservable();
  imageSelected$ = this.imageSource.asObservable();


  // Service message commands
  editState(state: string) {
    this.stateSource.next(state);
  }

  editCity(city: string) {
    this.citySource.next(city);
  }

  editSkillLevel(skillLevel: string) {
    this.skillLevel.next(skillLevel);
  }

  editImage(image: File) {
    this.imageSource.next(image);
  }
}
