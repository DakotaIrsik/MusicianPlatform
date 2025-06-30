import { throwError } from 'rxjs';
import { HttpErrorResponse, HttpParams } from '@angular/common/http';

export abstract class BaseService {
  constructor() { }

  protected handleError(err: HttpErrorResponse) {
    let errorMessage: string[] = [];
    if (err.error instanceof ErrorEvent) {
      errorMessage.push(`An error occured: ${err.error.message}`);
    } else {
      for (let er in err.error) {
        errorMessage.push(err.error[er]);
      }

    }

    console.error(errorMessage);
    return throwError(errorMessage);
  }

  toQueryString(options: any) {
    const params = new HttpParams();
    for (const key in options) {
      params.set(key, options[key]);
    }
  }
}
