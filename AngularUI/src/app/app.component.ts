import { Component, OnInit } from '@angular/core';

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

  constructor(private seatsService: SeatsService) { }

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
}
