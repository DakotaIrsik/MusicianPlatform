import { Component, OnInit, Input } from '@angular/core';
import { Room } from 'src/app/shared/models/room';
import { Studio } from 'src/app/shared/models/studio';
import { StudioService } from '../../studio.service';
import { Masks } from 'src/app/shared/masks';

@Component({
  selector: 'studio-portal-rooms',
  templateUrl: './rooms.component.html',
  styleUrls: ['./rooms.component.css']
})
export class RoomsComponent extends Masks implements OnInit {
  @Input() studio: Studio;
  room = new Room();

  constructor(private studioService: StudioService) {
    super();
   }

  ngOnInit() {
  }

  addRoom() {
    this.room.studioId = this.studio.id;
    this.studio.rooms.push(this.room);
    this.studioService.update(this.studio).subscribe(response => {
      this.studio = response;
      this.room = new Room();
    });
  }

  deleteRoom(roomId: number) {
    const roomToDelete = this.studio.rooms.find(r => r.id === roomId);
    roomToDelete.isActive = false;
    this.studioService.update(this.studio).subscribe(response => {
      this.studio = response;
      this.room = new Room();
    });
  }
}
