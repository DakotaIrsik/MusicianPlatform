import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpRequest } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { Studio } from '../../shared/models/studio';
import { StandardResponse } from 'src/app/shared/models/api/responses/standard-response';
import { StudioSearchRequest } from './studio-search/studio-search-request';
import { environment } from 'src/environments/environment';
import { BaseService } from 'src/app/shared/services/base-service';

@Injectable({
  providedIn: 'root'
})
export class StudioService extends BaseService {

  private baseUrl = environment.apiUrl + 'Studio';

  constructor(private http: HttpClient) {
    super();
   }

  search(studioSearchRequest: StudioSearchRequest) {
    return this.http.post<StandardResponse<Studio[]>>(this.baseUrl + '/Search', JSON.stringify(studioSearchRequest)).pipe(
      catchError(this.handleError));
  }

  mystudios(studioSearchRequest: StudioSearchRequest) {
    return this.http.post<StandardResponse<Studio[]>>(this.baseUrl + '/MyStudios', JSON.stringify(studioSearchRequest)).pipe(
      catchError(this.handleError));
  }

  create(studio: Studio) {
    return this.http.post<Studio>(this.baseUrl + '/CreateStudio', JSON.stringify(studio)).pipe(
      catchError(this.handleError));
  }

  update(studio: Studio) {
    return this.http.put<Studio>(this.baseUrl + '/UpdateStudio', JSON.stringify(studio)).pipe(
      catchError(this.handleError));
  }

  get(studioId: number) {
    return this.http.get<Studio>(this.baseUrl + '/' + studioId).pipe(
      catchError(this.handleError));
  }

  delete(studioId: number) {
    return this.http.delete(this.baseUrl + '/' + studioId).pipe(
      catchError(this.handleError));
  }
}
