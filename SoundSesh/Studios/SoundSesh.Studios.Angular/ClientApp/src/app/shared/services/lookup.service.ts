import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BaseService } from './base-service';
import { CitySearchRequest } from '../models/api/requests/city-search-request';
import { LookUp } from '../models/api/responses/lookup';

@Injectable({
  providedIn: 'root'
})
export class LookUpService extends BaseService {
  private baseUrl = environment.apiUrl;
  private cityApi = 'City';
  private socialMediaApi = 'SocialMedia';

  constructor(private http: HttpClient) {
    super();
  }

  get(api: string) {
    return this.http.get<LookUp[]>(this.baseUrl + api)
    .pipe(catchError(this.handleError));
  }

  getCities(state: string) {
    return this.http.post<Array<LookUp>>(this.baseUrl + this.cityApi, JSON.stringify(new CitySearchRequest(state)))
      .pipe(catchError(this.handleError));
  }

  getSocialMedia() {
    return this.http.get<Array<LookUp>>(this.baseUrl + this.socialMediaApi)
      .pipe(catchError(this.handleError));
  }
}

