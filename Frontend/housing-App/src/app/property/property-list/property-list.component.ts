import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HousingService } from 'src/app/services/housing.service';
import { ActivatedRoute } from '@angular/router';
import { IPropertyBase } from 'src/app/models/IPropertyBase';

@Component({
  selector: 'app-property-list',
  templateUrl: './property-list.component.html',
  styleUrls: ['property-list.component.css']
})
export class PropertyListComponent implements OnInit {

  City='';
  searchCity='';
  sortByParam;
  sortDirection='asc';

  SellRent=1;
  peoperties: IPropertyBase[];
  constructor(private route:ActivatedRoute,private housingService:HousingService) { }

  ngOnInit(): void {
    if(this.route.snapshot.url.toString()){
      this.SellRent=2;
    }
    this.housingService.getAllProperties(this.SellRent).subscribe(
      data=>{
        this.peoperties=data;
        // const newProperties=JSON.parse(localStorage.getItem('newProp'));
        // if(newProperties.SellRent===this.SellRent){
        //   this.peoperties=[newProperties, ...this.peoperties]
        // }
        console.log(data);
        // console.log(this.route.snapshot.url.toString());
      }, error=>{
        console.log("Http Error")
        console.log(error);
      }
    )
  }
  onCityFilter(){
    this.searchCity=this.City;
  }
  onClearFilter(){
    this.searchCity='';
    this.City='';
  }
  onSortDirection(){
    if(this.sortDirection==='desc'){
      this.sortDirection='asc';
    }else{
      this.sortDirection='desc';
    }

  }

}
