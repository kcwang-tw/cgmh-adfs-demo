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

  constructor() { }

  ngOnInit() { }
}
