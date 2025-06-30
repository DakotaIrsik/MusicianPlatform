import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Studio } from '../../shared/models/studio';
import { StandardResponse } from 'src/app/shared/models/api/responses/standard-response';
import { StudioSearchRequest } from './studio-search/studio-search-request';
import { BaseService } from 'src/app/shared/services/base-service';

@Injectable({
  providedIn: 'root'
})
export class StudioService extends BaseService {

  private studioApi = 'Studio';

  constructor(private http: HttpClient) {
    super();
   }

  search(request: StudioSearchRequest) {
    return this.http.post<StandardResponse<Studio[]>>(this.fullApiUrl + this.studioApi + '/Search', JSON.stringify(request)).pipe(
      catchError(this.handleError));
  }

  mystudios(request: StudioSearchRequest) {
    return this.http.post<StandardResponse<Studio[]>>(this.fullApiUrl + this.studioApi + '/MyStudios', JSON.stringify(request)).pipe(
      catchError(this.handleError));
  }

  create(studio: Studio) {
    return this.http.post<Studio>(this.fullApiUrl + this.studioApi + '/CreateStudio', JSON.stringify(studio)).pipe(
      catchError(this.handleError));
  }

  update(studio: Studio) {
    return this.http.put<Studio>(this.fullApiUrl + this.studioApi, JSON.stringify(studio)).pipe(
      catchError(this.handleError));
  }

  get(studioId: number) {
    return this.http.get<Studio>(this.fullApiUrl + this.studioApi + '/' + studioId).pipe(
      catchError(this.handleError));
  }

  delete(studioId: number) {
    return this.http.delete(this.fullApiUrl + this.studioApi + '/' + studioId).pipe(
      catchError(this.handleError));
  }
}
