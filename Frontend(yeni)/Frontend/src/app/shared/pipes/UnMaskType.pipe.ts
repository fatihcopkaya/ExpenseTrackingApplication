import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
  name: 'UnMaskType'
})
export class UnMaskTypePipe implements PipeTransform {

  transform(type: boolean): string {
    let result = "";

    switch (type) {
        case true:
            result = 'Evet';
            break;
        case false:
            result = 'Hayır';
            break;
        default:
            result = 'Belirtilmemiş';
            break;
    }

    return result;
  }
}
