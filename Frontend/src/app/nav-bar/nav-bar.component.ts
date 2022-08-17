import { Component, OnInit } from '@angular/core';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  loggedInUser!: string;

  constructor(private alertifyService: AlertifyService) { }

  ngOnInit() {
  }

  loggedIn() {
    this.loggedInUser = localStorage.getItem('token') || '';
    return this.loggedInUser;
  }

  onLogOut() {
    localStorage.removeItem('token');
    this.alertifyService.success('Logged Out Successfully');
  }
}
