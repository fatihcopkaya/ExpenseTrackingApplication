import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { AuthResponse } from '../model/auth-response';
import { CheckPermissionResponse } from '../model/check-permission-response';
import { environment } from '../../../environments/environment';
import { LoginDto } from '../model/loginDto';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  //private baseUrl:string ="http://localhost:5121"

  constructor(private http: HttpClient,private router: Router ) {}



//   getAppUserId(): string | null {
//     const token = localStorage.getItem('access-token');

//     if (token && !this.jwtHelper.isTokenExpired(token)) {
//       const decodedToken = this.jwtHelper.decodeToken(token);
//       return decodedToken ? decodedToken.appUserId : null;
//     }

//     return null;
// }
  // signIn(username: string, password: string)  {
  //  return this.http.post<any>(environment.bff_base_url +'/api/auth/login', {
  //   userName: username,
  //   password: password,
  //   rememberMe: false
  //  });
  // }

  // signUp(AppUserObj:any){
  //   return this.http.post<any>(`${this.baseUrl}AppUser`,AppUserObj)
  // }

  // signInOTP(userId: number, otpValue: string){
  //   return this.http.post<any>(environment.bff_base_url +'/api/auth/verifyloginotp', {
  //     userId: userId,
  //     otpCode: otpValue
  //    });
  // }




  onLogin(email: string, password: string){
    
     //return this.http.post<any>(`${this.baseUrl}/api/Authentication/Login`,loginObj)
     return this.http.post<any>(environment.bff_base_url +'/api/Authentication/Login',{email: email,
     password: password});
  }

// onLogin(email: string, password: string){
//     //debugger;

//     const headers = new HttpHeaders({
//        'Content-Type': 'application/json',
//        // Burada Authorization başlığına token ekleniyor
//        // Örneğin, Basic Auth veya Bearer Token kullanabilirsiniz.
//        // Bu örnek Bearer Token kullanımını göstermektedir.
//        'Authorization': 'Bearer ' + localStorage.getItem('access-token')
//      });

//      const body = {
//        email: email,
//        password: password
//      };
//     //return this.http.post<any>(`${this.baseUrl}/api/Authentication/Login`,loginObj)
//     return this.http.post<any>(`${environment.bff_base_url}/api/Authentication/Login`, body, { headers });
//  }

  forgotPassword(email: string){
    return this.http.post<any>(environment.bff_base_url +'/api/Authentication/Password-reset', {
      email: email
     });
  }

  verifyResetToken(resetToken:string,userId:string){
    return this.http.post<any>(environment.bff_base_url +'/api/Authentication/Verify-Reset-Token', {
      resetToken: resetToken,
      userId:userId
    });
  }
  updatePassword(userId:string,resetToken:string,password:string,passwordConfirm:string){

    return this.http.post<any>(environment.bff_base_url +'/api/Authentication/UpdatePassword',{
      userId:userId,
      resetToken: resetToken,
      password:password,
      passwordConfirm:passwordConfirm
    })
  }


  changePassword( oldPassword:string, newPassword:string, confirmPassword:string) {

    return this.http.post<any>(environment.bff_base_url + '/api/ChangePassword/ChangePassword', {
      OldPassword: oldPassword,
      NewPassword: newPassword,
      ConfirmPassword: confirmPassword
    });
  }




  signOut()
  {
    localStorage.removeItem('access-token');
    localStorage.removeItem('firstName');
    localStorage.removeItem('id');
    this.router.navigate(["/"]);
  }

  checkPermission(permission : string)
  {
    //debugger;
    return this.http.get<CheckPermissionResponse>(environment.bff_base_url +'/apiAuthentication/CheckPermission=' + permission)
  }
}
