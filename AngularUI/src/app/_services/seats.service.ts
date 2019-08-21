import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { DepartmentSeat } from '../_models/department-seat.model';
import { ApiResponse } from '../_models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class SeatsService {

  private baseUrl = environment.apiUrl + 'seats';

  constructor(private http: HttpClient) { }

  getAllSeats(): Observable<DepartmentSeat[]> {
    return this.http.get(this.baseUrl)
      .pipe(
        map((response: ApiResponse) => {
          return response.data;
        })
      );
  }
}
