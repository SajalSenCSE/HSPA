import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { User } from 'src/app/models/user';
import { AlertifyserviceService } from 'src/app/services/alertifyservice.service';
import { AuthService } from 'src/app/services/auth.service';




@Component({
  selector: 'app-user-register',
  templateUrl: './user-register.component.html',
  styleUrls: ['./user-register.component.css']
})
export class UserRegisterComponent implements OnInit {

  user:any={};
  registerationForm: FormGroup;
  userSubmited:boolean=false;

  constructor(private fb:FormBuilder,
              private authService:AuthService,
              private alertify:AlertifyserviceService) { }

  ngOnInit() {
    this.registerationForm=new FormGroup({
      userName: new FormControl(null,Validators.required),
      userEmail: new FormControl(null,[Validators.required,Validators.email]),
      password: new FormControl(null,[Validators.minLength(3),Validators.required]),
      conPass: new FormControl(null,[Validators.required,Validators.minLength(3)]),
      mobile:new FormControl(null,[Validators.required,Validators.minLength(2)])
    }, this.passwordNotMatchValidators);

    // this.createRegisterationForm();
  }



  ///I dont khow why it is not working
  createRegisterationForm(){
    this.registerationForm= this.fb.group({
      userName:[null,Validators.required],
      userEmail:[null,[Validators.required,Validators.email]],
      password:[null,[Validators.minLength(8),Validators.required]],
      conPass:[null,[Validators.required,Validators.minLength(8)]],
      modile:[null,[Validators.required,Validators.minLength(8)]]
    });
  }

  get userName(){
    return this.registerationForm.get('userName') as FormControl;
  }

  get userEmail(){
    return this.registerationForm.get('userEmail') as FormControl;
  }
  get password(){
    return this.registerationForm.get('password') as FormControl;
  }
  get conPass(){
    return this.registerationForm.get('conPass') as FormControl;
  }
  get mobile(){
    return this.registerationForm.get('mobile') as FormControl;
  }



   passwordNotMatchValidators(fg:FormGroup):Validators{
  return fg.get('password').value === fg.get('conPass').value ? null :{notMatch:true};
  }

  onSubmit(){
    this.userSubmited=true;

    if(this.registerationForm.valid){

      this.authService.registerUser(this.userData()).subscribe(()=>
      {
        this.registerationForm.reset();
        this.alertify.Success("Contratulation Sucessfully data save");
        this.userSubmited=false;
      }
      );
    }

  }
  userData():User {
    return this.user={
      userName:this.userName.value,
      userEmail:this.userEmail.value,
      password:this.password.value,
      mobile:this.mobile.value
    }
  }


}
