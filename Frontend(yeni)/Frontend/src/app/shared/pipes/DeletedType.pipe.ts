import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
  name: 'DeletedType'
})
export class DeletedTypePipe implements PipeTransform {

  transform(type: boolean): string {
    let result = "";

    switch (type) {
        case true:
            result = 'Kapatılmış';
            break;
        case false:
            result = 'Kapatılmamış';
            break;
        default:
            result = 'Belirtilmemiş';
            break;
    }

    return result;
  }
}
