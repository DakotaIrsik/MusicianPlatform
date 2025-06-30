import { Component, Input } from '@angular/core';
import { Studio } from 'src/app/shared/models/studio';
import { HoursOfOperationsService } from 'src/app/shared/forms/hours-of-operation-selection/hours-of-operation-service';
import { StudioService } from '../../studio.service';
import { BusinessHour } from 'src/app/shared/models/business-hour';

@Component({
  selector: 'studio-portal-hours',
  templateUrl: './hours.component.html',
  styleUrls: ['./hours.component.css'],
  providers: [HoursOfOperationsService]
})
export class HoursComponent {
  @Input()
  studio: Studio;
  error: string;
  hoursOfOperation: BusinessHour[] = [];

  constructor(private hoursOfOperationsService: HoursOfOperationsService,
              private studioService: StudioService) {
    this.hoursOfOperationsService.messageChanged$.subscribe(h => {
      this.updateHoo(this.hoursOfOperation, h);
    });
  }

  onSubmit() {
    this.hoursOfOperation.forEach(hoo => {
      this.updateHoo(this.studio.hoursofoperation, hoo);
    });
    this.studioService.update(this.studio).subscribe();
  }

  updateHoo(arrayToSearch: BusinessHour[], newValue: BusinessHour) {
    const existentHoo = arrayToSearch.find(existing => existing.dayOfWeek === newValue.dayOfWeek);
    if (existentHoo) {
      existentHoo.open = newValue.open;
      existentHoo.close = newValue.close;
      existentHoo.isOpen = newValue.isOpen;
    } else {
      arrayToSearch.push(newValue);
    }
  }

  getOpenTime(day: string): string {
    const existentDay = this.getDay(day);
    if (existentDay) {
      return existentDay.open;
    }
    return '';
  }

  getCloseTime(day: string): string {
    const existentDay = this.getDay(day);
    if (existentDay) {
      return existentDay.close;
    }
    return '';
  }

  isOpen(day: string): boolean {
    const existentDay = this.getDay(day);
    if (existentDay) {
      return existentDay.isOpen;
    }
    return false;
  }

  getDay(day: string): BusinessHour {
    return this.studio.hoursofoperation.find(hoo => hoo.dayOfWeek === day);
  }
}
