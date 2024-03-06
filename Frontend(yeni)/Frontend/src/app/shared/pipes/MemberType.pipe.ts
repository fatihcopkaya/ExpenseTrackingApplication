import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
  name: 'MemberType'
})
export class MemberTypePipe implements PipeTransform {

  transform(type: number): string {
    let result = "";

    switch (type) {
        case 0:
            result = 'Basic';
            break;
        case 1:
            result = 'Semiverified';
            break;
        case 2:
            result = 'Verified';
            break;
        case 3:
            result = 'Premium';
            break;
        default:
            result = 'Belirtilmemiş';
            break;
    }

    return result;
  }
}
