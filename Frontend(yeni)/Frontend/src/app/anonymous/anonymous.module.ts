import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AnonymousRoutingModule } from './anonymous-routing.module';

import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { CoreModule } from '../core/module/core.module';
import { ReactiveFormsModule } from '@angular/forms';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { LoginComponent } from './components/login/login.component';
import { UpdatePasswordComponent } from './components/update-password/update-password.component';






@NgModule({
    declarations: [
          LoginComponent,
          ForgotPasswordComponent,
          UpdatePasswordComponent

     ],
    imports: [
        RouterModule,
        CoreModule,
        AnonymousRoutingModule,
        CommonModule,
        SharedModule,
        ReactiveFormsModule


    ],
    providers:[


    ]
})
export class AnonymousModule { }
