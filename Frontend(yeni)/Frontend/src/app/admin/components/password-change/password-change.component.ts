import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/core/services/auth.service';
import { MessageService } from 'primeng/api';
import { ChangePasswordService } from 'src/app/services/change-password.service';
import { AuthResponse } from 'src/app/core/model/auth-response';
import { Router } from '@angular/router';

@Component({
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.scss']
})
export class PasswordChangeComponent implements OnInit {
  appuserid!: string;
  oldPassword: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  eyeIcon:string="pi-eye-slash";
  isText:boolean=false;
  type: string ="password";

  oldPasswordType: string = 'password';
  newPasswordType: string = 'password';
  confirmPasswordType: string = 'password';
  constructor(private authService: AuthService, private messageService: MessageService,private changepasswordService:ChangePasswordService,private router:Router) { }

  ngOnInit(): void {
    // this.handleChangePassword(); // Eğer sayfa yüklendiğinde çağrılmasını istemiyorsanız bu satırı kaldırın.
  }



  eyeIcons: { [key: string]: string } = {
    oldPassword: "pi-eye-slash",
    newPassword: "pi-eye-slash",
    confirmPassword: "pi-eye-slash"
  };

  isTexts: { [key: string]: boolean } = {
    oldPassword: false,
    newPassword: false,
    confirmPassword: false
  };

  passwordTypes: { [key: string]: string } = {
    oldPassword: "password",
    newPassword: "password",
    confirmPassword: "password"
  };

  hideShowPass(field: string) {
    this.isTexts[field] = !this.isTexts[field];
    this.eyeIcons[field] = this.isTexts[field] ? 'pi-eye' : 'pi-eye-slash';
    this.passwordTypes[field] = this.isTexts[field] ? 'text' : 'password';
  }


  handleChangePassword(): void {

    if (this.newPassword === this.confirmPassword) {
    this.authService.changePassword(this.oldPassword, this.newPassword, this.confirmPassword)
    .subscribe(
        result => {
            debugger;
            if (result&&result.isSuccess) {
                // Başarılı durumu
                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Şifre Değiştirme Başarılı', life: 3000 });
                this.router.navigate(['/login']);


            } else {
                // Hata durumu
                if (result.errors && result.errors.length > 0) {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
                } else {
                    this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Şifre değiştirilirken hata oluştu', life: 3000 });
                }
            }
        },
        error => {
            // API çağrısı sırasında bir hata durumu
            this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Failed to change password.', life: 3000 });
            console.error('Password change error:', error);
        }
    );
  }
 }
}
