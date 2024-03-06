import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserCountDTO } from '../model/usercount';

@Injectable({
  providedIn: 'root'
})
export class UserCountService {

  constructor(private http: HttpClient) { }

  getAppUserCount(){
    return this.http.get<any>(environment.bff_base_url + '/api/AppUser/GetAppUserCount');
  }
}
