import { Component, OnInit } from '@angular/core';

import { SeatingPlan } from './_models/seating-plan.model';
import { SeatingPlanService } from './_services/seating-plan.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {

  title = '座位表';
  seats: SeatingPlan[];

  constructor(private seatingPlanService: SeatingPlanService) { }

  ngOnInit() {
    this.getAllSeats();
  }

  getAllSeats() {
    this.seatingPlanService.getAllSeats().subscribe(
      response => {
        this.seats = response;
      }, error => {
        console.log(error);
      }
    );
  }
}
