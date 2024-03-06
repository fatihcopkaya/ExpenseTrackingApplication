import { Pipe, PipeTransform } from "@angular/core";
@Pipe({
    name: 'TransactionSuccesfull'
  })
  export class TransactionSuccesfullPipe implements PipeTransform {
    transform(type: boolean): string {
      let result = "";
  
      switch (type) {
          case true:
              result = 'Başarılı';
              break;
          case false:
              result = 'Başarısız';
              break;
              default:
                result = 'Belirtilmemiş';
                break;
      }
  
      return result;
    }
  }