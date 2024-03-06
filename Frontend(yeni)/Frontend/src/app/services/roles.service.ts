import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RoleDto } from '../model/roles';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RolesService {

  constructor(private http: HttpClient) { }


  createRoles(role:RoleDto){
    return this.http.post<any>(environment.bff_base_url + '/api/Role/CreatetRoles', role);
  }



  getRoles(roles:RoleDto) {
    return this.http.post<any>(environment.bff_base_url + '/api/Role/ListRoles',roles);
  }


}
