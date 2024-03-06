import { Pipe, PipeTransform } from "@angular/core";
@Pipe({
    name: 'isSent'
  })
  export class isSentPipe implements PipeTransform {
    transform(type: boolean): string {
      let result = "";
  
      switch (type) {
          case true:
              result = 'Gönderildi';
              break;
          case false:
              result = 'Gönderilmedi';
              break;
              default:
                result = 'Belirtilmemiş';
                break;
      }
  
      return result;
    }
  }