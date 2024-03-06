import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
  name: 'ApproveStatus'
})
export class ApproveStatusPipe implements PipeTransform {

  transform(type: number): string {
    let result = "";

    switch (type) {
      case 0:
        result = 'Onayda';
        break;
      case 1:
        result = 'Onaylandı';
        break;
      case 2:
        result = 'Reddedildi';
        break;
      default:
        result = 'Belirtilmemiş';
        break;
    }

    return result;
  }
}
