import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { PermissionDto } from '../model/permission';
import { ListPermissionByRolesDto } from '../model/listPermissionsByRoleId';
import { RoleDto } from '../model/roles';

@Injectable({
  providedIn: 'root'
})
export class PermissionService {

  constructor(private http: HttpClient) { }


  getPermissions(permission:PermissionDto) {


    return this.http.post<any>(environment.bff_base_url + '/api/Permission/ListPermission',permission);  //Burada observe: 'body' ile sadece veriyi almak istediğinizi belirtiyoruz.
}


  getPermissionsByRoleId(roleId: string) {
    return this.http.get<any>(environment.bff_base_url + `/api/Permission/ListPermissionByRoleId?RoleId=${roleId}`);
  }






}
