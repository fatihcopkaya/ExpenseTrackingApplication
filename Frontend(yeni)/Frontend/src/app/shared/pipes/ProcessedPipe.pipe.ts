import { Pipe, PipeTransform } from "@angular/core";
@Pipe({
    name: 'Processed'
  })
  export class ProcessedPipe implements PipeTransform {
    transform(type: boolean): string {
      let result = "";
  
      switch (type) {
          case true:
              result = 'İşlendi';
              break;
          case false:
              result = 'İşlenmedi';
              break;
              default:
                result = 'Belirtilmemiş';
                break;

      }
  
      return result;
    }
  }