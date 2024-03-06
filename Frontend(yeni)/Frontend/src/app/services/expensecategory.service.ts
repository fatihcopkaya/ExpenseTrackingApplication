import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PaymentFilter } from '../model/paymentFilter';
import { expenseCategoryFilter } from '../model/expensecategoryFilter';
import { expensecategoryDto } from '../model/expensecategory';

@Injectable({
  providedIn: 'root'
})
export class ExpenseCategoryService {


  constructor(private http: HttpClient) { }



  getExpenseCategories() {
    
    return this.http.get<any>(environment.bff_base_url + '/api/controller/ListExpenseCategory');  //Burada observe: 'body' ile sadece veriyi almak istediğinizi belirtiyoruz.
  }
  
}
