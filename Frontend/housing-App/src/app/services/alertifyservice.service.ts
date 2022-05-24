import { Injectable } from '@angular/core';

import *as alertify from 'alertifyjs'

@Injectable({
  providedIn: 'root'
})
export class AlertifyserviceService {

constructor() { }

  Success(message){
    alertify.success(message);
  }
  Errors(message){
    alertify.error(message);
  }

}
