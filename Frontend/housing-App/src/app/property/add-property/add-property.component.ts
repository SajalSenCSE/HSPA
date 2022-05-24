import { Component, OnInit, ViewChild } from '@angular/core';
import { TabHeadingDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { FormBuilder, FormControl, FormGroup, NgForm, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IPropertyBase } from 'src/app/models/IPropertyBase';
import { flatMap } from 'rxjs';
import { Property } from 'src/app/models/Property';
import { HousingService } from 'src/app/services/housing.service';
import { AlertifyserviceService } from 'src/app/services/alertifyservice.service';
import { IkeyValuePiar } from 'src/app/models/IkeyValuePiar';
import { DatePipe } from '@angular/common';


@Component({
  selector: 'app-add-property',
  templateUrl: './add-property.component.html',
  styleUrls: ['./add-property.component.css']
})
export class AddPropertyComponent implements OnInit {

  // @ViewChild('Form') addPropertyForm:NgForm;

  @ViewChild('formTabSets') formTabSets: TabsetComponent;
  addPropertyForm:FormGroup;
  NextTab:boolean;
  property=new Property();
  cityList:any[];

  propertyView:IPropertyBase ={
    id: null,
    sellRent: null,
    name: null,
    propertyType: null,
    furnishingType: null,
    price: null,
    bhk: null,
    builtArea: null,
    city: '',
    readyToMove: null

  };

  propertyType: IkeyValuePiar[];
  furnishType:IkeyValuePiar[];
  areaZoon:Array<string>=["East","West","South","North"];


  constructor(private fb:FormBuilder,
              private router:Router,
              private hServices:HousingService,
              private alertify:AlertifyserviceService,
              private datePipe:DatePipe) { }

  ngOnInit(): void {

    if(!localStorage.getItem('userName')){
      this.alertify.Errors("You must loged for adding property");
      this.router.navigate(['/user/login']);

    }
    this.createAddPropertyForm();
    this.hServices.getAllCities().subscribe(
      data=>{
        this.cityList=data;
        // console.log(data);
      }
    )
    this.hServices.getPropertyTypes().subscribe(
      data=>{
       this.propertyType=data;
      }
    )
    this.hServices.getFurnishingTypes().subscribe(
      data=>{
       this.furnishType=data;
      }
    )
  }

  createAddPropertyForm(){
    this.addPropertyForm=this.fb.group({
        Basicinfo:this.fb.group({
        SellRent:[1,Validators.required],
        BHK:[null,Validators.required],
        PType:[null,Validators.required],
        FType:[null,Validators.required],
        Name:[null,Validators.required],
        City:[null,Validators.required]
      }),
      PriceInfo:this.fb.group({
        Price:[null,Validators.required],
        BuiltArea:[null,Validators.required],
        Security:[0],
        Maintain:[0],
        CarpetArea:[null]
      }),
      AddressInfo:this.fb.group({
        FloorNo:[null],
        Address:[null,Validators.required],
        TotalFloor:[null],
        LandMark:[null]
      }),
      OtherInfo:this.fb.group({
        RTM:[null,Validators.required],
        PossessionOn:[null,Validators.required],
        Ageproperty:[null],
        GetedCommunity:[null],
        Area:[null],
        Description:[null]
      }),
      Photo:this.fb.group({

      })
    });
  }




// GetterMethon>


  get BasicInfo(){
    return this.addPropertyForm.get('Basicinfo') as FormGroup;
  }
  get PriceInfo(){
    return this.addPropertyForm.get('PriceInfo') as FormGroup;
  }
  get AddressInfo(){
    return this.addPropertyForm.get('AddressInfo') as FormGroup;
  }
  get OtherInfo() {
    return this.addPropertyForm.get('OtherInfo') as FormGroup;
  }
  get Photo(){
    return this.addPropertyForm.get('Photo') as FormGroup;
  }



  // gettermethod for from control

  get SellRent(){
    return this.BasicInfo.get('SellRent') as FormControl;
  }
  get BHK(){
    return this.BasicInfo.get('BHK') as FormControl;
  }
  get Name(){
    return this.BasicInfo.get('Name') as FormControl;
  }
  get PType(){
    return this.BasicInfo.get('PType') as FormControl;
  }
  get FType(){
    return this.BasicInfo.get('FType') as FormControl;
  }
  get City(){
    return this.BasicInfo.get('City') as FormControl;
  }


  get Price(){
    return this.PriceInfo.get('Price') as FormControl;
  }
  get BuiltArea(){
    return this.PriceInfo.get('BuiltArea') as FormControl;
  }
  get Security(){
    return this.PriceInfo.get('Security') as FormControl;
  }
  get Maintain(){
    return this.PriceInfo.get('Maintain') as FormControl;
  }
  get CarpetArea(){
    return this.PriceInfo.get('CarpetArea') as FormControl;
  }


  get Address(){
    return  this.AddressInfo.get('Address') as FormControl;
  }
  get FloorNo(){
    return this.AddressInfo.get('FloorNo') as FormControl;
  }
  get TotalFloor(){
    return this.AddressInfo.get('TotalFloor') as FormControl;
  }
  get LandMark(){
    return this.AddressInfo.get('LandMark') as FormControl;
  }

  get RTM(){
    return this.OtherInfo.get('RTM') as FormControl;
  }
  get PossessionOn(){
    return this.OtherInfo.get('PossessionOn') as FormControl;
  }
  get Ageproperty(){
    return this.OtherInfo.get('Ageproperty') as FormControl;
  }
  get GetedCommunity(){
    return this.OtherInfo.get('GetedCommunity') as FormControl;
  }
  get Area(){
    return this.OtherInfo.get('Area') as FormControl;
  }
  get Description(){
    return this.OtherInfo.get('Description') as FormControl;
  }

  onBack(){
    this.router.navigate(['/']);
  }
  onSubmited(){
    this.NextTab=true;
    if(this.allTabsValid()){
      this.mapProperty();
      this.hServices.addProperty(this.property).subscribe(
        ()=>{
            this.alertify.Success("Congratulaion your property was sucessfully added on website");
            console.log(this.addPropertyForm);
            if(this.SellRent.value==='2'){
              this.router.navigate(['/rent-property']);
            }else{

              this.router.navigate(['/']);
            }
         }
      );
    }
    else{
      console.log("Review From");
      this.alertify.Errors("Faild");
    }
  }

  mapProperty():void{
    this.property.id=this.hServices.newPropID();
    this.property.sellRent=+this.SellRent.value;
    this.property.bhk=this.BHK.value;
    this.property.propertyTypeId=this.PType.value;
    this.property.name=this.Name.value;
    this.property.cityId=this.City.value;
    this.property.furnishingTypeId=this.FType.value;
    this.property.price=this.Price.value;
    this.property.security=this.Security.value;
    this.property.maintenance=this.Maintain.value;
    this.property.builtArea=this.BuiltArea.value;
    this.property.carpetArea=this.CarpetArea.value;
    this.property.floorNo=this.FloorNo.value;
    this.property.totalFloors=this.TotalFloor.value;
    this.property.address=this.Address.value;
    this.property.address2=this.LandMark.value;
    this.property.readyToMove=this.RTM.value;
    // this.property.age=this.Ageproperty.value;
    this.property.gated=this.GetedCommunity.value;
    this.property.mainEntrance=this.Area.value;
    this.property.estPossessionOn=this.datePipe.transform(this.PossessionOn.value,'MM/dd/yyyy');
    this.property.description=this.Description.value;
    // this.property.PostedOn=new Date().toString();
  }

  allTabsValid(): boolean{

    if(this.BasicInfo.invalid){
      this.formTabSets.tabs[0].active = true;
      return false;
    }
    if(this.PriceInfo.invalid){
      this.formTabSets.tabs[1].active = true;
      return false;
    }
    if(this.AddressInfo.invalid){
      this.formTabSets.tabs[2].active=true;
      return false;
    }
    if(this.OtherInfo.invalid){
      this.formTabSets.tabs[3].active=true;
      return false;
    }
    if(this.Photo.invalid){
      return false;
    }
    return true;
  }
  selectTab(tabId: number,IsCurrentTab:boolean) {
    this.NextTab=true;
    if(IsCurrentTab){
      this.formTabSets.tabs[tabId].active = true;
      this.NextTab=false;
    }
  }
}
