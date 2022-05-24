import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, concatMap, Observable, of, retry, retryWhen, throwError } from "rxjs";
import { ErrorCode } from "../enum/enums";
import { AlertifyserviceService } from "./alertifyservice.service";

@Injectable({
  providedIn:'root'
})
export class HttpErrorInterceptorService implements HttpInterceptor{

  constructor(private alertify:AlertifyserviceService){}

  intercept(req: HttpRequest<any>, next: HttpHandler){
    console.log("Http Request Started");
    return next.handle(req).pipe(
      retryWhen((error:Observable<any>)=>this.retryRequest(error,10)),
      catchError((error:HttpErrorResponse)=>{
        const errorMessage=this.serError(error);
        console.log(error);
        this.alertify.Errors(errorMessage);
        return throwError(errorMessage);
      })
    );

  }


  //retry block errors ..

  retryRequest(error:Observable<unknown>,retryCount:number):Observable<unknown>{
    return error.pipe(concatMap((checkErr:HttpErrorResponse,count:number)=>{
      if(count<=retryCount){
        switch(checkErr.status){
          case ErrorCode.serverDown:
            return of(checkErr);

          // case ErrorCode.unauthroze:
          //   return of(checkErr);
        }
      }
      return throwError(checkErr);
    }
    ))
  }

  serError(error:HttpErrorResponse):string{
    let errorMessage='Unknown Error Occurce';
    if(error.error instanceof ErrorEvent){
      //Client Side Error Block
      errorMessage=error.error.message;
    }else{

        if(error.status===401){
          return error.statusText;
        }
          //Server Side Error
        if(error.status !== 0){
          errorMessage=error.error;
        }
    }
    return errorMessage;
  }
}
