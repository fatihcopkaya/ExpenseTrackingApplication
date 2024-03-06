import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserInformationDTO } from '../model/userInformation';


@Injectable({
  providedIn: 'root'
})
export class ProfileInfoService {

  constructor(private http: HttpClient) { }

    getProfileInfo(){
      
      return this.http.get<any>(environment.bff_base_url + '/api/ProfileInfo/GetProfileInfoById');// bu metot kalkabilir id ye göre değil authentica olmuş kullanıcı gelecek

    }

    updateProfileInfo(profileInfo:UserInformationDTO){

      return this.http.put<any>(environment.bff_base_url + '/api/ProfileInfo/UpdateProfileInfo', profileInfo);
    }
}
