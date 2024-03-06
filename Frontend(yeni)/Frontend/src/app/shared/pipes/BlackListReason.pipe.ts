import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
  name: 'BlackListReason'
})
export class BlackListReasonPipe implements PipeTransform {

  transform(type: number): string {
    let result = "";

    switch (type) {
        case 0:
            result = 'None';
            break;
        case 1:
            result = 'Cancel Topup';
            break;
        default:
            result = 'Belirtilmemiş';
            break;
    }

    return result;
  }
}
