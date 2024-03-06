import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { NameInfoDto } from '../model/topbar-name';

@Injectable({
  providedIn: 'root'
})
export class AppTopbarService {

  constructor(private http: HttpClient) { }

  getFirstAndLastName() {
     //debugger;
    return this.http.get<any>(environment.bff_base_url+'/api/AppUser/GetNameByAuthenticatedUser');
  }

  
}
