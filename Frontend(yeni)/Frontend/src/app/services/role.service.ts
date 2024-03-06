import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { RoleDto } from '../model/roles';
import { RoleFilter } from '../model/roleFilter';
import { BaseResultPagination } from '../core/model/base-result-pagination';
import { PaginationService } from './pagination-service';

@Injectable({
  providedIn: 'root'
})
export class RoleService implements PaginationService<RoleDto> {

  constructor(private http: HttpClient) { }


  getPagination(filter: RoleFilter): Observable<BaseResultPagination<RoleDto>> {
    debugger;
    return this.http.post<BaseResultPagination<RoleDto>>(environment.bff_base_url + '/api/Role/ListRolesForPagination', filter);
  }

  getRoleInfo(roles:RoleDto){
    return this.http.post<any>(environment.bff_base_url + '/api/Role/ListRoles',roles);
  }
  createRoleInfo(roles:RoleDto){

    return this.http.post<any>(environment.bff_base_url + '/api/Role/CreateRoles', roles);
  }
  deleteRoleInfo(id?: string) {
    debugger;

    return this.http.delete<any>(environment.bff_base_url + `/api/Role/DeleteRoles?Id=${id}`);
  }
  updateRoleInfo(roles:RoleDto){


    return this.http.put<any>(environment.bff_base_url + '/api/Role/UpdateRoles', roles);
  }
}

