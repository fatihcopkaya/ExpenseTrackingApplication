import { Pipe, PipeTransform } from "@angular/core";


@Pipe({
  name: 'PassiveReason'
})
export class PassiveReasonPipe implements PipeTransform {

  transform(type: number): string {
    let result = "";

    switch (type) {
        case 0:
            result = '';
            break;
        case 1:
            result = 'AdminUpdate';
            break;
        case 2:
            result = 'CashoutTry';
            break;
        case 3:
            result = 'CashoutSuccess';
            break;
        case 4:
            result = 'CancelOrChargebackTopup';
            break;
        case 5:
            result = 'SendToIBANFail';
            break;
        case 101:
            result = 'AccountCloseRequest';
            break;
        case 102:
            result = 'AccountFreeze';
            break;
        case 103:
            result = 'AccountStolen';
            break;
        case 104:
            result = 'IdleMSISDN';
            break;
        case 105:
            result = 'ParentFreeze';
            break;
        default:
            result = 'Belirtilmemiş';
            break;
    }

    return result;
  }
}
