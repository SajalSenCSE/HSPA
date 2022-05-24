import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'sort'
})
export class SortPipe implements PipeTransform {

  transform(value: Array<String>, args: any[]): any {
    const sortedFild=args[0];
    const sortDirection = args[1];
    let multiplay=1;
    if(sortDirection === 'desc'){
      multiplay=-1;
    }
    if(value){
      value.sort((a:any,b:any)=>{
        if(a[sortedFild] < b[sortedFild]){
          return -1 * multiplay;
        }else if(a[sortedFild] > b[sortedFild]){
          return 1 * multiplay;
        }else{
          return 0;
        }
      })
      return value;
    }
  }

}
