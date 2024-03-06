import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { UserRoleDto } from '../model/userRole';
import { UserRoleFilterDto } from '../model/userRoleFilter';
import { BaseResultPagination } from '../core/model/base-result-pagination';
import { PaginationService } from './pagination-service';

@Injectable({
  providedIn: 'root'
})
export class UserRoleService implements PaginationService<UserRoleDto> {

  constructor(private http: HttpClient) { }


  getPagination(filter: UserRoleFilterDto): Observable<BaseResultPagination<UserRoleDto>> {
    return this.http.post<BaseResultPagination<UserRoleDto>>(environment.bff_base_url + '/api/UserRole/ListUserRole', filter);
  }

  createUserRole(userRole:UserRoleDto){

    return this.http.post<any>(environment.bff_base_url + '/api/UserRole/CreateUserRole', userRole);
  }

  deleteUserRole(id?:string) {
    //debugger;
    return this.http.delete<any>(environment.bff_base_url + `/api/UserRole/DeleteUserRole?Id=${id}`);
  }
  
  updateUserRole(userRole:UserRoleDto){


    return this.http.put<any>(environment.bff_base_url + '/api/UserRole/UpdateUserRole', userRole);
  }
  

}
