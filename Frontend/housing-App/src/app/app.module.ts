import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { Routes,RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';


import { AppComponent } from './app.component';
import { PropertyCardComponent } from './property/property-card/property-card.component';
import { PropertyListComponent } from './property/property-list/property-list.component';
import { NavBarComponent } from './nav-bar/nav-bar.component';
import { AddPropertyComponent } from './property/add-property/add-property.component';
import { PropertyDetailComponent } from './property/property-detail/property-detail.component';
import { UserLoginComponent } from './user/user-login/user-login.component';
import { UserRegisterComponent } from './user/user-register/user-register.component';
import { HousingService } from './services/housing.service';
import { AuthService } from './services/auth.service';
import { PropertyDetailResolverService } from './property/property-detail/property-detail-resolver.service';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { FilterPipe } from './Pipes/filter.pipe';
import { SortPipe  } from './Pipes/sort.pipe';
import { HttpErrorInterceptorService } from './services/httperror-interceptor.service';
import { DatePipe } from '@angular/common';



const appRoutes:Routes=[
  {path:'',component:PropertyListComponent},
  {path:'rent-property',component:PropertyListComponent},
  {path:'add-property',component:AddPropertyComponent},
  {path:'property-detail/:id',
          component:PropertyDetailComponent,
          resolve:{prp:PropertyDetailResolverService}},

  {path:'user/login',component:UserLoginComponent},
  {path:'user/register',component:UserRegisterComponent},
  {path:'**',redirectTo:'',pathMatch:'full'}
]


@NgModule({
  declarations: [
    AppComponent,
    PropertyCardComponent,
    PropertyListComponent,
    NavBarComponent,
    AddPropertyComponent,
    PropertyDetailComponent,
    UserLoginComponent,
    UserRegisterComponent,
    FilterPipe,
    SortPipe
   ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(appRoutes),
    BrowserAnimationsModule,
    TabsModule.forRoot(),
    BsDropdownModule.forRoot(),
    ButtonsModule.forRoot(),
    BsDatepickerModule.forRoot(),
    NgxGalleryModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass:HttpErrorInterceptorService,
      multi:true

    },
    DatePipe,
    HousingService,
    AuthService,
    PropertyDetailResolverService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
