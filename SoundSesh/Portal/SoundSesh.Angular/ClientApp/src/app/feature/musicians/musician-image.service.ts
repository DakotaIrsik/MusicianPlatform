import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { BaseService } from 'src/app/shared/services/base-service';
import { MusicianFileInformation } from './musician.file';
import { Musician } from 'src/app/shared/models/musician';
import { FileInformation } from 'src/app/shared/models/file';

@Injectable({
  providedIn: 'root'
})
export class MusicianImageService extends BaseService {

  constructor(private http: HttpClient) {
    super();
   }

  private baseUrl = environment.musicianApi.url + environment.musicianApi.version + 'Image';

  uploadImage(file: File, sfi: FileInformation) {
    const formData = new FormData();
    formData.append('file', file, file.name);
    return this.http.post<FileInformation>(this.baseUrl + '?subType=' + sfi.subType + '&fileType=' + sfi.fileType, formData)
      .pipe(catchError(this.handleError));
  }
}
