import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.scss']
})
export class UpdatePasswordComponent implements OnInit {
  password: string = '';
  passwordConfirm: string = '';
  isText:boolean=false;
  type: string ="password";
  userId: string= '';
  resetToken:string = '';
  eyeIcon:string="pi-eye-slash";


  constructor(private router: Router,private activatedRoute:ActivatedRoute, private authService: AuthService, private messageService: MessageService) { }

  ngOnInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      this.userId = params['userId'];
      this.resetToken = params['resetToken'];
    });

    //this.updatePassword();// Burada gerekiyorsa giriş yapmış kullanıcıyı kontrol edebilirsiniz
    // Örneğin: this.authService.getUser().subscribe(user => { /* Kullanıcı bilgilerini işle */ });
  }
  
 
  hideShowPass(field: string) {
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "pi-eye" : this.eyeIcon = "pi-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";

    if (field === 'password') {
        this.type = this.isText ? 'text' : 'password';
    } else if (field === 'passwordConfirm') {
        this.type = this.isText ? 'text' : 'password';
    }
}






  updatePassword(){
    
    if (this.password === this.passwordConfirm) {
      this.activatedRoute.params.subscribe(params =>{
        const userId:string=params["userId"];
        const resetToken:string=params["resetToken"];
        this.authService.updatePassword(userId,resetToken,this.password,this.passwordConfirm).subscribe(result=>{
        if (result.isSuccess){

          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Şifre Başarıyla Güncellendi.', life: 3000 });


        }
        else
        this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });


        })
      });
      this.router.navigate(["/login"]);
    }



    }




  
  cancelUpdate(){
    this.router.navigate(["/login"]);
  }

}
