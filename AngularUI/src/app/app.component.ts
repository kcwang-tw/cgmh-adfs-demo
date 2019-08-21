import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { DepartmentSeat } from './_models/department-seat.model';
import { SeatsService } from './_services/seats.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = '座位表';
  seats: DepartmentSeat[];
  newSeatForm: FormGroup;

  constructor(private seatsService: SeatsService) {
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
    console.log(this.newSeatForm.value);
  }
}
