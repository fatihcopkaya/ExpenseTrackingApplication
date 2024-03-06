import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
  name: 'ApprovalType'
})
export class ApprovalTypePipe implements PipeTransform {

  transform(type: number): string {
    let result = "";

    switch (type) {
      case 1:
        result = 'Ekleme';
        break;
      case 2:
        result = 'Güncelleme';
        break;
      case 3:
        result = 'Silme';
        break;
      default:
        result = 'Belirtilmemiş';
        break;
    }

    return result;
  }
}
