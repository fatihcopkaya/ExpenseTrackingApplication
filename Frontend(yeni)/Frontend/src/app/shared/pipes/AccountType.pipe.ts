import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
  name: 'AccountType'
})
export class AccountTypePipe implements PipeTransform {

  transform(type: number): string {
    let result = "";

    switch (type) {
        case 0:
            result = 'Tip1';
            break;
        case 1:
            result = 'Tip2';
            break;
        default:
            result = 'Belirtilmemiş';
            break;
    }

    return result;
  }
}
