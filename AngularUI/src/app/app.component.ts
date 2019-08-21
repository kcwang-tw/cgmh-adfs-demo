import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { DepartmentSeat } from './_models/department-seat.model';
import { RanksService } from './_services/ranks.service';
import { SeatsService } from './_services/seats.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = '座位表';
  userRank: string;
  seats: DepartmentSeat[];
  newSeatForm: FormGroup;

  constructor(private rankService: RanksService, private seatsService: SeatsService) {
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
}
