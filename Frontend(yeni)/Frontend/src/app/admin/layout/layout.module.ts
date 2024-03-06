import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CoreLayoutModule } from 'src/app/core/module/core-layout.module';
import { RouterModule } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';
import { SharedModule } from 'src/app/shared/shared.module';
import { AppConfigModule } from 'src/app/core/config/config.module';
import { SidebarModule } from 'primeng/sidebar';
import { AdminLayoutComponent } from './layout.component';
import { CardModule } from 'primeng/card';
 import { HomeComponent } from '../components/home/home.component'; // BUaray gelince bak
import { AdduserComponent } from '../components/adduser/adduser.component';





@NgModule({
    declarations: [
        AdminLayoutComponent,
        HomeComponent,





    ],
    imports: [
        CoreLayoutModule,
        SidebarModule,
        AppConfigModule,
        FormsModule,
        HttpClientModule,
        SharedModule,
        MenubarModule,
        RouterModule,
        CardModule,
        BrowserAnimationsModule,
        BrowserModule,



    ],
    exports: [AdminLayoutComponent]
})
export class AdminLayoutModule { }
