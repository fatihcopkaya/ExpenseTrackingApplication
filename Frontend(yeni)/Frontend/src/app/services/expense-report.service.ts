import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ListExpenseInfoFilter } from '../model/expenseInfoFilter';
import { Observable } from 'rxjs';
import { BaseResultPagination } from '../core/model/base-result-pagination';
import { ListExpenseDto } from '../model/expenseInfo';
import { environment } from 'src/environments/environment';
import { PaginationService } from './pagination-service';

@Injectable({
  providedIn: 'root'
})
export class ExpenseReportService implements PaginationService<ListExpenseDto> {

  constructor(private http: HttpClient) { }


    getPagination(filter: ListExpenseInfoFilter): Observable<BaseResultPagination<ListExpenseDto>> {

        return this.http.post<BaseResultPagination<ListExpenseDto>>(environment.bff_base_url + '/api/Expense/GetExpenseForReportList', filter);
    }

//   getForExpensePagination(filter: ListExpenseInfoFilter): Observable<BaseResultPagination<ListExpenseDto>> {

//     debugger;
//     return this.http.post<BaseResultPagination<ListExpenseDto>>(environment.bff_base_url + '/api/Expense/GetExpenseForReportList', filter);
//   }
}
