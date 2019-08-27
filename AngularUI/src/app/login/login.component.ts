import { Component, OnInit } from '@angular/core';
import { AdalService } from '../_services/adal.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private adalService: AdalService) { }

  ngOnInit() {
  }

  login() {
    this.adalService.login();
  }
}
