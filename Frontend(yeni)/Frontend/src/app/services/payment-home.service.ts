import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaymentListDto } from '../model/paymentInfo';
import { PaymentInfoFilter } from '../model/paymentInfoFilter';
import { WalletDto } from '../model/wallet';

@Injectable({
  providedIn: 'root'
})
export class PaymentHomeService {

  constructor(private http: HttpClient) { }

  getTotalAmount(expenseCategoryType:number){
    return this.http.get<any>(environment.bff_base_url + '/api/Payment/getTotalAmount?ExpenseCategoryType='+ expenseCategoryType);
  }
  getTotalPaymentAndExpense(){
   
    return this.http.get<any>(environment.bff_base_url + '/api/Wallet/payments-and-expenses');
  }


}
