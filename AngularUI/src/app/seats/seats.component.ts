import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { OAuthService } from 'angular-oauth2-oidc';

import { DepartmentSeat } from '../_models/department-seat.model';
import { RanksService } from '../_services/ranks.service';
import { SeatsService } from '../_services/seats.service';

@Component({
  selector: 'app-seats',
  templateUrl: './seats.component.html',
  styleUrls: ['./seats.component.scss']
})
export class SeatsComponent implements OnInit {

  userRank = '9';
  seats: DepartmentSeat[];
  newSeatForm: FormGroup;
  name: string;

  constructor(private rankService: RanksService,
              private seatsService: SeatsService,
              private oauthService: OAuthService) {
    this.newSeatForm = new FormGroup({
      userId: new FormControl('', Validators.required),
      userName: new FormControl('', Validators.required),
      extension: new FormControl('', Validators.required),
      phone: new FormControl('', Validators.required),
      seat: new FormControl('', Validators.required)
    });
  }

  ngOnInit() {
    this.getAllSeats();
    this.getName();
  }

  getRank(userId: string) {
    this.rankService.getUserRank(userId).subscribe(
      response => {
        this.userRank = response.rank;
      }
    );
  }

  getAllSeats() {
    this.seatsService.getAllSeats().subscribe(
      response => {
        this.seats = response;
      }, error => {
        console.log(error);
      }
    );
  }

  onSubmit() {
    this.seatsService.addNewSeat(this.newSeatForm.value).subscribe(
      response => {
        console.log(response);
        this.newSeatForm.reset();
        this.getAllSeats();
      }
    );

  }

  removeSeat(userId: string) {
    this.seatsService.removeSeat(userId).subscribe(
      response => {
        console.log(response);
        this.getAllSeats();
      }
    );

  }

  getName() {
    const claims = this.oauthService.getIdentityClaims();
    console.log(claims);
  }

  logout() {
    this.oauthService.logOut();
  }
}
