import { Injectable } from '@angular/core';
import { UserInformationDTO } from '../model/userInformation';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { UserInformationFilter } from '../model/userInformationFilter';
import { BaseResultPagination } from '../core/model/base-result-pagination';
import { userInfo } from 'os';
import { PaginationService } from './pagination-service';
import { UserExpenseCategoryInfo } from '../model/userExpenseCategoryInfo';

@Injectable({
  providedIn: 'root'
})
export class UserInformationService implements PaginationService<UserInformationDTO>{

  constructor(private http: HttpClient) { }

  // getPagination(filter: UserInformationFilter): Observable<BaseResultPagination<UserInformationDTO>> {
  //   return this.http.post<BaseResultPagination<UserInformationDTO>>(environment.bff_base_url + '/api/AppUser/GetAppUsersByExpenseCategory', filter);
  // }
  getPagination(filter: UserInformationFilter): Observable<BaseResultPagination<UserInformationDTO>> {
    return this.http.post<BaseResultPagination<UserInformationDTO>>(environment.bff_base_url + '/api/AppUser/GetAppUsers', filter);
  }


    createUserInfo(users:UserInformationDTO){

    return this.http.post<any>(environment.bff_base_url + '/api/AppUser/AddAppUser', users);
    }

    deleteUserInfo(id?: string) {
        return this.http.delete<any>(environment.bff_base_url + `/api/AppUser/DeleteAppUser?Id=${id}`);
      }


    updateUserInfo(users:UserInformationDTO){

    return this.http.put<any>(environment.bff_base_url + '/api/AppUser/UpdateAppUser', users);
    }
    getUserNameById(){
      return this.http.get<any>(environment.bff_base_url +  '/api/AppUser/GetAppUserName')
    }

    getUserNameForPayment(users:UserInformationDTO){
      return this.http.post<any>(environment.bff_base_url +  '/api/AppUser/GetAppUsersForPayment',users)
    }









}

