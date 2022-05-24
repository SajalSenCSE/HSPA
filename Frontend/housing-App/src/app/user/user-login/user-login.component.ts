import { Component, OnInit } from '@angular/core';
import { NgForm, Validator } from '@angular/forms';
import { Router } from '@angular/router';
import { UserForLogin } from 'src/app/models/user';
import { AlertifyserviceService } from 'src/app/services/alertifyservice.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-user-login',
  templateUrl: './user-login.component.html',
  styleUrls: ['./user-login.component.css']
})
export class UserLoginComponent implements OnInit {

  constructor(private userAuth:AuthService,
    private alertify:AlertifyserviceService,
    private router:Router) { }

  ngOnInit() {
  }
  onLogin(loginForm:NgForm){
    console.log(loginForm.value);
    this.userAuth.loginUser(loginForm.value).subscribe(
      (resposce:UserForLogin)=>{
        console.log(resposce);
        const user=resposce;
        localStorage.setItem('token',user.token);
        localStorage.setItem('userName',user.userName);
        this.alertify.Success("Login Sucessfully");
        this.router.navigate(['/']);
      }
    );
  }

}
