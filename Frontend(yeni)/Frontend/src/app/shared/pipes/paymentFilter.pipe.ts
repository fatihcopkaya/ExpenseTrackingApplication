import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'paymentFilter'
  })

  export class PaymentFilterPipe implements PipeTransform {
    transform(value: any, userFilter: string, monthFilter: string): any {
      // Filtreleme mantığını burada uygulayın ve sonucu döndürün
      // Örnek bir filtreleme yapısı:
      if (userFilter || monthFilter) {
        return value.filter((payment: any) => {
          if (userFilter && payment.user !== userFilter) {
            return false;
          }
          if (monthFilter && payment.month !== monthFilter) {
            return false;
          }
          return true;
        });
      } else {
        return value;
      }
    }
  }
