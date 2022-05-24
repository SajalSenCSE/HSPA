
export interface User {
  userName:string;
  userEmail:string;
  password:string;
  mobile:number;
}
export interface UserForLogin {
  userName:string;
  password:string;
  token:string;
}
export interface UserForRegister {
  userName:string;
  password:string;
}
