import { Pipe, PipeTransform } from "@angular/core";
@Pipe({
    name: 'LoginSuccesfull'
  })
  export class LoginSuccesfullPipe implements PipeTransform {
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