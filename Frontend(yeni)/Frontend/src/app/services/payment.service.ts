import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ListPaymentDto } from '../model/payment';
import { BaseResultPagination } from '../core/model/base-result-pagination';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaginationService } from './pagination-service';
import { PaymentListDto } from '../model/paymentInfo';
import { PaymentInfoFilter } from '../model/paymentInfoFilter';

@Injectable({
    providedIn: 'root'
  })
  export class PaymentService implements PaginationService<PaymentListDto> {

    constructor(private http: HttpClient) { }

    
    getPagination(filter: PaymentInfoFilter): Observable<BaseResultPagination<PaymentListDto>> {
        debugger;
        const filteredUrl = (environment.bff_base_url + '/api/Payment/list');
        return this.http.post<BaseResultPagination<PaymentListDto>>(filteredUrl, filter);

        //return this.http.post<BaseResultPagination<ListPaymentDto>>(environment.bff_base_url + '/api/Payment/list', filter);
    }

    
    createPayments(payment:PaymentListDto){
        return this.http.post<any>(environment.bff_base_url + '/api/Payment/CreatePayment', payment);
    }

    updatePayments(payment:PaymentListDto){
        return this.http.put<any>(environment.bff_base_url + '/api/Payment/UpdatePayment', payment);
    }

    deletePayment(id?: string) {
        return this.http.delete<any>(environment.bff_base_url + `/api/Payment/DeletePayment?Id=${id}` );
    }
    createNewPeriod(payment:PaymentListDto){
        return this.http.post<any>(environment.bff_base_url + '/api/Payment/create-new-period', payment);
    }





}

