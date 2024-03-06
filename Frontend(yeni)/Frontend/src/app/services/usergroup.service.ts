import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { BaseResultPagination } from '../core/model/base-result-pagination';
import { userFilter } from '../model/userfilter';
import { UserGroupFilter } from '../model/usergroupfilter';
import { UserGroupListDto } from '../model/usergroupspg';
import { UserListDto } from '../model/userspg';
import { PaginationService } from './pagination-service';

@Injectable({
    providedIn: 'root'
  })
export class UserGroupService implements PaginationService<UserGroupListDto>{

    constructor(private http: HttpClient) { }

    getPagination(filter: UserGroupFilter): Observable<BaseResultPagination<UserGroupListDto>> {
        return this.http.post<BaseResultPagination<UserGroupListDto>>(environment.bff_base_url + '/api/v1/getusergroups', filter);
      }

   createUserGroup(usergroup:UserGroupListDto)
   {
    return this.http.post<any>(environment.bff_base_url + '/api/v1/createusergroup', usergroup);
   }

   updateUserGroup(usergroup:UserGroupListDto)
   {
    return this.http.put<any>(environment.bff_base_url + '/api/v1/updateusergroup', usergroup);
   }

   deleteUserGroup(id?:number)
   {
    return this.http.delete<any>(environment.bff_base_url + '/api/v1/deleteusergroup/'+id);
   }

}

