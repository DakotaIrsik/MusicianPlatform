import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Musician } from '../../shared/models/Musician';
import { BaseService } from 'src/app/shared/services/base-service';

@Injectable({
  providedIn: 'root'
})
export class MusicianService extends BaseService {

  musicianApi = 'Musician/';
  constructor(private http: HttpClient) {
    super();
   }

  create(musician: Musician) {
    return this.http.post<Musician>(this.fullApiUrl + this.musicianApi + 'CreateMusician', JSON.stringify(musician));
  }

  update(musician: Musician) {
    return this.http.put<Musician>(this.fullApiUrl + this.musicianApi, JSON.stringify(musician));
  }

  get() {
    return this.http.get<Musician>(this.fullApiUrl + this.musicianApi);
  }
}
