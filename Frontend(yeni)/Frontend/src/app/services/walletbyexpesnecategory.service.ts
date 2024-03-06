import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { walletByExpenseCategoryDto } from '../model/walletByExpenseCategory';
import { walletByExpenseCategoryFiter } from '../model/walletByExpenseCategoryfilter';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators'; // Burayı ekleyin

@Injectable({
  providedIn: 'root'
})
export class WalletbyexpesnecategoryService {

  constructor( private http:HttpClient) { }

  GetWalletByExpenseCategory() {

    return this.http.get<any>(environment.bff_base_url + "/api/WalletByExpenseCategory/GetExpenseByIdAsync");
  }

  getAllWalletByCategoryByExpenseCategoryId() {

    return this.http.get<walletByExpenseCategoryDto>(environment.bff_base_url +"/api/WalletByExpenseCategory/GetAllWalletByCategoryByExpenseCategoryId");
  }
}
