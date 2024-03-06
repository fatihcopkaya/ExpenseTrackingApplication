import { Component, OnInit } from '@angular/core';
import{FormBuilder,FormGroup,Validators,FormControl} from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { MessageService } from 'primeng/api';
import { LayoutService } from 'src/app/core/services/app.layout.service';
import { LoginDto } from 'src/app/core/model/loginDto';
import { CaptchaCheck } from 'src/app/core/model/check-captcha'; 
import { AuthzGuard } from 'src/app/core/guard/authz.guard';
import { log } from 'console';
import { CaptchaService } from 'src/app/core/services/captcha.service';
import { Captcha } from 'src/app/core/model/captcha';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
 type: string ="password";
 isText:boolean=false;
 eyeIcon:string="pi-eye-slash";
 password!: string;
 email!:string;
 Id!: string; 
 captchaCode!:string;
 captcha:Captcha=new Captcha();

 firstName!:string;
 lastName!:string;
 login: LoginDto = new LoginDto();

  loginForm!: FormGroup;
  showForgotPasswordDialog: boolean = false;
  forgotPasswordEmail: string = ''; // E-posta alanını tutan bir değişken
 constructor(private fb:FormBuilder,private layoutService:LayoutService,private authService:AuthService,private router:Router,private _captchaService: CaptchaService,private messageService: MessageService){ }
 ngOnInit(): void {
  this.loginForm = this.fb.group({
    email:[this.login.email,Validators.required],
    password:[this.login.password,Validators.required],
    captchaCode: [this.captchaCode, Validators.required]
  })
  this.getCaptcha();
 }
  hideShowPass(){
    this.isText=!this.isText;
    this.isText ? this.eyeIcon="pi-eye": this.eyeIcon="pi-eye-slash";
    this.isText ? this.type="text":this.type="password";
  }
  

  
  openForgotPasswordDialog() {
        this.showForgotPasswordDialog = true; // Parolayı sıfırlama formunu göstermek için bir değişkeni true olarak ayarladık.
        //this.forgotPasswordEmail = ''; // E-posta alanını temizleyin veya sıfırlayın
        this.router.navigate(['/forgotPassword']);

  }


  onLogin() {
    
    if (this.loginForm.valid) {
        if (this.captchaCode) {
            let checkCaptcha = new CaptchaCheck();
            checkCaptcha.captchaCode = this.captchaCode;
            checkCaptcha.captchaValue = this.captcha.captchaValue;

            this._captchaService.checkCaptcha(checkCaptcha).subscribe(p => {
                if (p.isSuccess) {
                    this.authService.onLogin(this.loginForm.value.email, this.loginForm.value.password).subscribe(response => {
                        if (response.message=="Login to the system is successful.") {
                          
                            localStorage.setItem('accessToken', response.tokenVM.accessToken);
                            localStorage.setItem('firstName', response.firstName);
                            localStorage.setItem('lastName', response.lastName);
                            //localStorage.setItem('firstName', getNameFromToken(response.tokenVM)); 
                            this.router.navigate(['/admin/home']);
                        }
                    });
                }
            });
            this.getCaptcha();
            this.captchaCode = '';
        }
    } else {
        this.validateAllFormFields(this.loginForm);
        alert("Your form is invalid!");
    }
}

    




  private validateAllFormFields(formGroup: FormGroup) {
      Object.keys(formGroup.controls).forEach(field => {
        const control = formGroup.get(field);
        if (control instanceof FormControl) {
          control.markAsDirty({ onlySelf: true });
        } else if (control instanceof FormGroup) {
          this.validateAllFormFields(control);
        }
      });
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
