import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { BaseResult, BaseResultData } from "../model/base-result";
import { Captcha } from "../model/captcha";
import { CaptchaCheck } from "../model/check-captcha";
@Injectable({
    providedIn: 'root'
  })
  export class CaptchaService{
  
    constructor(private http: HttpClient) { }
    getCaptcha(): Observable<BaseResultData<Captcha>> {
        return this.http.get<BaseResultData<Captcha>>(environment.bff_base_url + '/api/Captcha/Get-Captcha');
    }
    checkCaptcha(captchaCheck:CaptchaCheck): Observable<BaseResult> {
      return this.http.post<BaseResult>(environment.bff_base_url + '/api/Captcha/Verify-Captcha', captchaCheck);
    }
}