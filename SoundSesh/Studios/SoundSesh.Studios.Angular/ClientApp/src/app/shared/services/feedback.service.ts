import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BaseService } from 'src/app/shared/services/base-service';
import { Feedback } from '../models/feedback';
import { StandardResponse } from '../models/api/responses/standard-response';
import { Paging } from '../models/api/paging';

@Injectable({
  providedIn: 'root'
})
export class FeedbackService extends BaseService {

  private baseUrl = environment.apiUrl + 'Feedback';

  constructor(private http: HttpClient) {
    super();
   }

  create(feedback: Feedback) {
    return this.http.post<Feedback>(this.baseUrl, JSON.stringify(feedback)).pipe(
      catchError(this.handleError));
  }

  search() {
    const paging = new Paging();
    paging.size = 1500;
    return this.http.post<StandardResponse<Feedback[]>>(this.baseUrl + '/Search', JSON.stringify(paging)).pipe(
      catchError(this.handleError));
  }
}
