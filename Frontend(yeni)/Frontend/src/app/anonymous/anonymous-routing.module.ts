import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { LoginComponent } from './components/login/login.component';
import { UpdatePasswordComponent } from './components/update-password/update-password.component';





@NgModule({
    imports: [RouterModule.forChild([
        { path: '', component: LoginComponent},
        { path: 'login', component: LoginComponent},
        { path: 'forgotPassword', component: ForgotPasswordComponent},
        { path: 'updatePassword/:userId/:resetToken', component: UpdatePasswordComponent}
    ])],
    exports: [RouterModule]
})
export class AnonymousRoutingModule { }
