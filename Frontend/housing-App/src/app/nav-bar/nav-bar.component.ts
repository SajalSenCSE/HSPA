import { Component, OnInit } from '@angular/core';
import { AlertifyserviceService } from '../services/alertifyservice.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  loggedinUser:string;
  constructor(private alertyfy:AlertifyserviceService) { }


  ngOnInit() {
  }

  loggedin(){
     this.loggedinUser= localStorage.getItem('userName');
     return this.loggedinUser;
  }
  onLogout(){
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    this.alertyfy.Success("You are loggedOut");
  }

}
