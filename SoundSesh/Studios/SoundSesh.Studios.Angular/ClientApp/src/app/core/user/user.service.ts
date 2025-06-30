import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { BaseService } from 'src/app/shared/services/base-service';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {

  private baseUrl = environment.apiUrl;
  private userApi = 'User';

  constructor(private http: HttpClient) {
    super();
   }

  associateIdentityUserToApplication() {
    return this.http.get(this.baseUrl + this.userApi)
        .pipe(catchError(this.handleError));
  }
}
