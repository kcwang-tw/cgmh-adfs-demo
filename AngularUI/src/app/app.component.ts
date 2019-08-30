import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { DepartmentSeat } from './_models/department-seat.model';
import { RanksService } from './_services/ranks.service';
import { SeatsService } from './_services/seats.service';

import * as AuthenticationContext from 'adal-angular';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  private authContext;

  title = '座位表';
  userRank = '9';
  seats: DepartmentSeat[];
  newSeatForm: FormGroup;

  constructor(
    private rankService: RanksService, 
    private seatsService: SeatsService,
    ) {
   
    this.configure();
    this.newSeatForm = new FormGroup({
      userId: new FormControl('', Validators.required),
      userName: new FormControl('', Validators.required),
      extension: new FormControl('', Validators.required),
      phone: new FormControl('', Validators.required),
      seat: new FormControl('', Validators.required)
    });
  }

  private configure() {

    this.authContext = new AuthenticationContext({  
      instance: 'https://adfs2016.southeastasia.cloudapp.azure.com/', // your STS URL
      tenant: 'adfs',                      // this should be adfs
      redirectUri : window.location.origin,
      //cacheLocation: 'localStorage',
      clientId: 'e1cf1ac7-0462-4b48-bc99-b8d425a2d2e3',
      endpoints: {
        "https://localhost:44326": "https://localhost:44326", // API URL , ResourceID
      }
      // //popUp: false,
      // callback : (errorDesc, token, error, tokenType) => 
      // {
      //   console.log(token);
      //   console.log(tokenType);
      // }
      }
    );
  }

  ngOnInit() {
    var isCallback = this.authContext.isCallback(window.location.hash);
    if (isCallback) {
        this.authContext.handleWindowCallback();
    }


    var user = this.authContext.getCachedUser();
    if (user) {
        // Use the logged in user information to call your own api
        //onLogin(null, user);
        console.log(user);
        let that = this;
        this.authContext.acquireToken('https://localhost:44326', function (errorDesc, token, error) {
          console.log('errorDesc:' + errorDesc);
          console.log('token:' + token);
          console.log('error:' + error)
          that.seatsService.getAllSeats(token).subscribe(
            response => {
              console.log(response);
              //that.authContext.logOut();
              //console.log(user);
            }, error => {
              console.log('Response Error');
              console.log(error);
            }
          );
          if (error) { //acquire token failure
              console.log('in error')
              this.authContext.acquireTokenRedirect('https://localhost:44326', null, null);
              }
          else {
              //acquired token successfully
          }
        });
    }
    else {
        // Initiate login
        this.authContext.login();
    }    
  }

  // getRank(userId: string) {
  //   this.rankService.getUserRank(userId).subscribe(
  //     response => {
  //       this.userRank = response.rank;
  //     }
  //   );
  // }

  // getAllSeats(token) {
  //   this.seatsService.getAllSeats(token).subscribe(
  //     response => {
  //       this.seats = response;
  //     }, error => {
  //       console.log(error);
  //     }
  //   );

  // };

  // onSubmit() {
  //   this.seatsService.addNewSeat(this.newSeatForm.value).subscribe(
  //     response => {
  //       console.log(response);
  //       this.newSeatForm.reset();
  //       this.getAllSeats(null);
  //     }
  //   );

  // }

  // removeSeat(userId: string) {
  //   this.seatsService.removeSeat(userId).subscribe(
  //     response => {
  //       console.log(response);
  //       this.getAllSeats(null);
  //     }
  //   );

  // }
}
