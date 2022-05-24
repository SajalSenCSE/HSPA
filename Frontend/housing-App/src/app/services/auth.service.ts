import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserForLogin, UserForRegister } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { }
  loginUser(user:UserForLogin){
    return this.http.post('http://localhost:5000/api/account/login',user);
  }
  registerUser(user:UserForRegister){
    return this.http.post('http://localhost:5000/api/account/register',user);
  }


}
