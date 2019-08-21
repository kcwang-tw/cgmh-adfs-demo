import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { SeatingPlan } from '../_models/seating-plan.model';
import { ApiResponse } from '../_models/api-response.model';

@Injectable({
  providedIn: 'root'
})
export class SeatingPlanService {

  private baseUrl = environment.apiUrl + 'seating-plan';

  constructor(private http: HttpClient) { }

  getAllSeats(): Observable<SeatingPlan[]> {
    return this.http.get(this.baseUrl)
      .pipe(
        map((response: ApiResponse) => {
          return response.data;
        })
      );
  }
}
