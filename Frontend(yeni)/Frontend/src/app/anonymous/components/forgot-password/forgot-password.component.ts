import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { MessageService } from 'primeng/api';
import { UserInformationDTO } from 'src/app/model/userInformation';
import { CaptchaService } from 'src/app/core/services/captcha.service';
import { Captcha } from 'src/app/core/model/captcha';
import { CaptchaCheck } from 'src/app/core/model/check-captcha';
@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit {

  forgotPasswordEmail!: string;
  userId!:string ;
  infoMessage: boolean = false;
  captchaCode!:string;
  captcha:Captcha=new Captcha();
  constructor(private router:Router, private authService:AuthService, private messageService: MessageService,private _captchaService: CaptchaService) {

  }

  ngOnInit(): void {
    this.getCaptcha();
  }

  cancelForgotPassword(){
    this.router.navigate(["login"]);
  }

  forgotPassword() {
    debugger;
    if (this.captchaCode) {
      let checkCaptcha = new CaptchaCheck();
      debugger;
      checkCaptcha.captchaCode = this.captchaCode;
      checkCaptcha.captchaValue = this.captcha.captchaValue;
      this._captchaService.checkCaptcha(checkCaptcha).subscribe(p => {
        if (p.isSuccess) {
          debugger;
          this.authService.forgotPassword(this.forgotPasswordEmail).subscribe(response => {
            if (response.isSuccess) {
              this.userId = response.userId;
              debugger;
              this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: 'Mailinizi Kontrol Edin.', life: 3000 });

             setTimeout(() => {
              this.router.navigate(['/login']);
              }, 3000); //3000 yapılıcak sonra
            } else {
              this.messageService.add({ severity: 'error', summary: "Login İşlemi", detail: response.errors ? response.errors.join(',') : 'İşlem başarısız.', life: 3000 });
            }
           
          });
        }
      });
      this.getCaptcha();
      this.captchaCode = '';
    }
  }
    


 

getCaptcha(){
  this._captchaService.getCaptcha().subscribe(p => {
      if(p.isSuccess){
          this.captcha = <Captcha>p.data
          this.captcha.captchaImage = 'data:image/jpg;base64,' + this.captcha.captchaImage;
      }
  });
}



 

}
