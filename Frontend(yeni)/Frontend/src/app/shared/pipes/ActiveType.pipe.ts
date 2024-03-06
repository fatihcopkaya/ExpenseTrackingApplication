import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
  name: 'ActiveType'
})
export class ActiveTypePipe implements PipeTransform {

  transform(type: boolean): string {
    let result = "";

    switch (type) {
        case true:
            result = 'Aktif';
            break;
        case false:
            result = 'Pasif';
            break;
        default:
            result = 'Belirtilmemiş';
            break;
    }

    return result;
  }
}
