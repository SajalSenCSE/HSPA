import { Injectable } from '@angular/core';
import {HttpClient, HttpHandler, HttpHeaders} from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { IPropertyBase } from '../models/IPropertyBase';
import { Property } from '../models/Property';
import { IkeyValuePiar } from '../models/IkeyValuePiar';


@Injectable({
  providedIn: 'root'
})
export class HousingService {

  constructor(private http:HttpClient) { }

  getAllCities():Observable<string[]>{
    return this.http.get<string[]>('http://localhost:5000/api/city');
  }

  getPropertyTypes():Observable<IkeyValuePiar[]>{
    return this.http.get<IkeyValuePiar[]>('http://localhost:5000/api/propertytype/list');
  }
  getFurnishingTypes():Observable<IkeyValuePiar[]>{
    return this.http.get<IkeyValuePiar[]>('http://localhost:5000/api/furnishingtype/list');
  }


  getProperty(id:number){
    return this.http.get<Property>('http://localhost:5000/api/property/detail/'+id.toString());
  }

  getAllProperties(SellRent?:number):Observable<Property[]>{
    return this.http.get<Property[]>('http://localhost:5000/api/property/list/'+SellRent.toString());
  }

  addProperty(property:Property){
    const httpOptions={
      headers:new HttpHeaders({
        Authorization:'Bearer '+localStorage.getItem('token')
      })
    };
    return this.http.post('http://localhost:5000/api/property/add',property,httpOptions);
  }

  newPropID() {
    if (localStorage.getItem('PID')) {
      localStorage.setItem('PID', String(+localStorage.getItem('PID') + 1));
      return +localStorage.getItem('PID');
    } else {
      localStorage.setItem('PID', '101');
      return 101;
    }
  }

  getPropertyAge(estDateofPro:string):string{
    const today=new Date();
    const estDate=new Date(estDateofPro);
    let age=today.getFullYear()-estDate.getFullYear();
    const m=today.getMonth()-estDate.getMonth();

    if(m<0||(m===0 && today.getDate()<estDate.getDate())){
      age--;
    }
    if(today<estDate){
      return '0';
    }
    if(age==0){
      return 'less than a year';
    }
    return age.toString();
  }


}
