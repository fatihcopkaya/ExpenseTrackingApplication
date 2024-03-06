import { Pipe, PipeTransform } from "@angular/core";
@Pipe({
    name: 'Refund'
  })
  export class RefundPipe implements PipeTransform {
    transform(type: boolean): string {
      let result = "";
  
      switch (type) {
        case true:
            result = 'İade Edildi';
            break;
        case false:
            result = 'İade Edilmedi';
            break;
            default:
              result = 'Belirtilmemiş';
              break;

      }
  
      return result;
    }
  }