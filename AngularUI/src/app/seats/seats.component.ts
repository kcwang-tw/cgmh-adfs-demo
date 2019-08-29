import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { DepartmentSeat } from '../_models/department-seat.model';
import { RanksService } from '../_services/ranks.service';
import { SeatsService } from '../_services/seats.service';
import { AdalService } from '../adal/adal.service';

@Component({
  selector: 'app-seats',
  templateUrl: './seats.component.html',
  styleUrls: ['./seats.component.scss']
})
export class SeatsComponent implements OnInit {

  userRank = '9';
  userInfo: any;

  seats: DepartmentSeat[];
  newSeatForm: FormGroup;

  constructor(private rankService: RanksService,
              private seatsService: SeatsService,
              private adalService: AdalService) {
    this.newSeatForm = new FormGroup({
      userId: new FormControl('', Validators.required),
      userName: new FormControl('', Validators.required),
      extension: new FormControl('', Validators.required),
      phone: new FormControl('', Validators.required),
      seat: new FormControl('', Validators.required)
    });
  }

  ngOnInit() {
    this.getUserInfo();
    this.getAllSeats();
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

  getUserInfo() {
    console.log(this.adalService.userInfo.profile);
    this.userInfo = this.adalService.userInfo.profile;
  }

  logOut() {
    this.adalService.logOut();
  }
}
