import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ListExpenseDto } from '../model/expenseInfo';
import { ListExpenseInfoFilter } from '../model/expenseInfoFilter';
import { BaseResultPagination } from '../core/model/base-result-pagination';
import { ExpenseTypeDto } from '../model/expenseType';
import { ExpenseTypeFilterDto } from '../model/expenseTypeFilter';


@Injectable({
  providedIn: 'root'
})
export class ExpenseInfoService {

  constructor(private http: HttpClient) { }

  getPagination(filter: ListExpenseInfoFilter): Observable<BaseResultPagination<ListExpenseDto>> {
    return this.http.post<BaseResultPagination<ListExpenseDto>>(environment.bff_base_url + '/api/Expense/GetExpenseList', filter);
  }
  createExpenseInfo(expenseInfo:ListExpenseDto){

    return this.http.post<any>(environment.bff_base_url + '/api/Expense/CreateExpense', expenseInfo);
  }

  deleteExpenseInfo(id?: string) {

    return this.http.delete<any>(environment.bff_base_url + `/api/Expense/DeleteExpense?Id=${id}`);
  }

  updateExpenseInfo(expenseInfo:ListExpenseDto){

    return this.http.post<any>(environment.bff_base_url + '/api/Expense/UpdateExpense', expenseInfo);
  }
  getExpenseTypes(expenseType:ExpenseTypeDto) {

    return this.http.post<any>(environment.bff_base_url + '/api/ExpenseType/ListExpenseType',expenseType);  //Burada observe: 'body' ile sadece veriyi almak istediğinizi belirtiyoruz.
  }
  createExpenseType(expenseType:ExpenseTypeDto){
   

    return this.http.post<any>(environment.bff_base_url + '/api/ExpenseType/CreateExpenseType', expenseType);
  }

}
