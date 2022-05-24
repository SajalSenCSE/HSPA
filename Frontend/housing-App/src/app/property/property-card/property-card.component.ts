import { Component, Input } from "@angular/core";
import { IPropertyBase } from "src/app/models/IPropertyBase";


@Component({
  selector:'app-property-card',
  templateUrl:'property-card.component.html',
  styleUrls:['property-card.component.css']
})

export class PropertyCardComponent{

  @Input() Property: IPropertyBase;
  @Input() hidenIcon:boolean;
}
