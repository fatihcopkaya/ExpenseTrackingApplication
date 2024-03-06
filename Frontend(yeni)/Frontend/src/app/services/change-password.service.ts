import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { changePasswordDto } from '../model/changePassword';

@Injectable({
  providedIn: 'root'
})
export class ChangePasswordService {

    constructor(private http: HttpClient) { }


    // changePassword( changePassword:changePasswordDto){

    //     return this.http.put<any>(environment.bff_base_url + '/api/ChangePassword/ChangePassword', changePassword);
    // }


    changePassword(appUserId: string, oldPassword: string, newPassword: string) {
        return this.http.post<any>(environment.bff_base_url+'/api/ChangePassword/ChangePassword', {
          AppUserId: appUserId,
          OldPassword: oldPassword,
          NewPassword: newPassword,
          ConfirmPassword: newPassword // Assuming you have a ConfirmPassword field in the backend
        });
      }
}
