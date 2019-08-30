import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders  } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from 'src/environments/environment';
import { DepartmentSeat } from '../_models/department-seat.model';
import { ApiResponse } from '../_models/api-response.model';

//import { OAuthService } from 'angular-oauth2-oidc';

import * as AuthenticationContext from 'adal-angular';

@Injectable({
  providedIn: 'root'
})
export class SeatsService {

  private baseUrl = environment.apiUrl + 'seats';
  
  constructor(private http: HttpClient) { }

  getAllSeats(token): Observable<DepartmentSeat[]> {

    let headers = new HttpHeaders({
     'Authorization': `Bearer ${token}`
    });
    let options = {
      headers
    };
    //return this.http.get(this.baseUrl, this.getAuthHeader(token))
    return this.http.get(this.baseUrl, options)
      .pipe(
        map((response: any) => {
          return response.data;
        })
      );
  }

  // protected getAuthHeader(token): any {
  //   //let token = 'qe';//this.oauthService.getAccessToken();
    
  //   return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  // }

  // addNewSeat(newSeat: DepartmentSeat): Observable<any> {
  //   return this.http.post(this.baseUrl, newSeat)
  //     .pipe(
  //       map((response: ApiResponse) => {
  //         return response.data;
  //       })
  //     );
  // }

  // removeSeat(userId: string): Observable<any> {
  //   return this.http.delete(this.baseUrl + `/${userId}`)
  //     .pipe(
  //       map((response: ApiResponse) => {
  //         return response.data;
  //       })
  //     );
  // }
}
