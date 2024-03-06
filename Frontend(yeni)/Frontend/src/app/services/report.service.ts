import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseResultPagination } from '../core/model/base-result-pagination';
import { PaymentListDto } from '../model/paymentInfo';
import { environment } from 'src/environments/environment';
import { PaymentFilter } from '../model/paymentFilter';
import { Observable } from 'rxjs';
import { ExpenseInfoService } from './expense-info.service';
import { ListExpenseInfoFilter } from '../model/expenseInfoFilter';
import { ListExpenseDto } from '../model/expenseInfo';

@Injectable({
  providedIn: 'root'
})
export class ReportService {

  constructor(private http: HttpClient) { }



  getPagination(filter: PaymentFilter): Observable<BaseResultPagination<PaymentListDto>> {
    debugger;
    return this.http.post<BaseResultPagination<PaymentListDto>>(environment.bff_base_url + '/api/Payment/Reportlist', filter);
  }

 

}
