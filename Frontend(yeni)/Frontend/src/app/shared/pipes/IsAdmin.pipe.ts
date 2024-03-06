import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
  name: 'IsAdminType'
})
export class IsAdminTypePipe implements PipeTransform {

  transform(type: number): string {
    let result = "";

   if (type==1)
    result="Evet"
   else
    result="Hayır" 

    return result;
  }
}
