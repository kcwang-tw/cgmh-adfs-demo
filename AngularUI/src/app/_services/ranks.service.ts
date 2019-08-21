import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ApiResponse } from '../_models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class RanksService {

  private baseUrl = environment.apiUrl + 'ranks';

  constructor(private http: HttpClient) { }

  getUserRank(userId: string): Observable<any> {
    return this.http.get(this.baseUrl + `/${userId}`)
      .pipe(
        map((response: ApiResponse) => {
          return response.data;
        })
      );
  }
}
