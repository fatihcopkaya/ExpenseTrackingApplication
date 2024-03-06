import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RolePermissionFilter } from '../model/rolePermissionFilter';
import { RolePermissionDto } from '../model/rolePermission';
import { BaseResultPagination } from '../core/model/base-result-pagination';
import { environment } from 'src/environments/environment';
import { Observable, catchError, throwError } from 'rxjs';
import { PaginationService } from './pagination-service';
import { CreateRolePermissionsDto } from '../model/createRolePermissions';

@Injectable({
  providedIn: 'root'
})
export class RolePermissionService implements  PaginationService<RolePermissionDto> {

  constructor(private http:HttpClient) { }

  getPagination(filter: RolePermissionFilter): Observable<BaseResultPagination<RolePermissionDto>> {
    return this.http.post<BaseResultPagination<RolePermissionDto>>(environment.bff_base_url + '/api/RolePermission/ListRolePermission', filter);
  }


  getRolePermissions(rolePermission:RolePermissionDto) {

    return this.http.post<any>(environment.bff_base_url + '/api/RolePermission/ListRolePermission',rolePermission);  //Burada observe: 'body' ile sadece veriyi almak istediğinizi belirtiyoruz.
}



  createRolePermission(rolePermissions:RolePermissionDto){


    return this.http.post<any>(environment.bff_base_url + '/api/RolePermission', rolePermissions);
    }




    deleteRolePermission(roleId: string, permissionIds: string[]) {
       debugger;
       const formattedPermissionIds = permissionIds.join(',');

        return this.http.delete<any>(environment.bff_base_url + `/api/RolePermission/DeleteRolePermission?RoleId=${roleId}&PermissionIds=${formattedPermissionIds}`);
      }



  updateRolePermission(rolePermissions:RolePermissionDto){


    return this.http.put<any>(environment.bff_base_url + '/api/RolePermission/UpdateRolePermission', rolePermissions);
    }
}
