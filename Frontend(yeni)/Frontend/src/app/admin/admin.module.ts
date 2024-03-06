import { NgModule } from '@angular/core';
import { CommonModule, DatePipe, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { AdminDashboardComponent } from './components/dashboard/dashboard.component';
import { AdminRoutingModule } from './admin-routing.module';
import { EmptyDemoComponent } from './components/empty/emptydemo.component';
import { CoreModule } from '../core/module/core.module';



import { ConfirmationService } from 'primeng/api';
import { CardModule } from 'primeng/card';
import { AdduserComponent } from './components/adduser/adduser.component';
import { UserListComponent } from './components/user-list/user-list.component';
import { PaymentsComponent } from './components/payments/payments.component';
import { ReportComponent } from './components/report/report.component';
import { PaymentInformationsComponent } from './components/payment-informations/payment-informations.component';
import { SettingsComponent } from './components/settings/settings.component';
import { PasswordChangeComponent } from './components/password-change/password-change.component';
import { DataViewModule, DataViewLayoutOptions } from 'primeng/dataview';
import { TableModule } from 'primeng/table';
import { ProfileComponent } from './components/profile/profile.component';
import { AddRoleComponent } from './components/add-role/add-role.component';
import { ChartModule } from 'primeng/chart';
import { PanelMenuModule } from 'primeng/panelmenu';
import { ButtonModule } from 'primeng/button';
import { MenuModule } from 'primeng/menu';
import { FormsModule } from '@angular/forms';
//import { FormsModule } from '@angular/forms';
import { PaymentFilterPipe } from '../shared/pipes/paymentFilter.pipe';
import { DropdownModule } from 'primeng/dropdown';
import { DialogModule } from 'primeng/dialog';
import { PaymentFootballComponent } from './components/payment-football/payment-football.component';
import { PaymentsCoffeeComponent } from './components/payments-coffee/payments-coffee.component';
import { UserinfomationCoffeeComponent } from './components/userinfomation-coffee/userinfomation-coffee.component';
import { UserInformationFootballComponent } from './components/user-information-football/user-information-football.component';
import {MatSelectModule} from '@angular/material/select';
import { ExpenseInfoComponent } from './components/expense-info/expense-info.component';
// import { UserRoleListComponent } from './components/user-role-list/user-role-list.component';
import { ExpenseReportComponent } from './components/expense-report/expense-report.component';
import { RolePermissionsComponent } from './components/role-permissions/role-permissions.component';














@NgModule({
    declarations: [
        AdminDashboardComponent,
        EmptyDemoComponent,
        AdduserComponent,
        UserListComponent,

        ReportComponent,
        PaymentInformationsComponent,
        SettingsComponent,
        PasswordChangeComponent,
        PaymentsComponent,
        ProfileComponent,
        AddRoleComponent,
        PaymentFilterPipe,
        PaymentFootballComponent,
        PaymentsCoffeeComponent,
        UserinfomationCoffeeComponent,
        UserInformationFootballComponent,
        ExpenseInfoComponent,
       // UserRoleListComponent,
        ExpenseReportComponent,
        RolePermissionsComponent






    ],
    imports: [
        RouterModule,
        SharedModule,
        AdminRoutingModule,
        CommonModule,
        CoreModule,
        CardModule,
        DataViewModule,
        TableModule,
        ChartModule,
        PanelMenuModule,
        ButtonModule,
        FormsModule,
        MenuModule,
        DropdownModule,
        DialogModule,
        MatSelectModule



    ],
    providers: [



     ConfirmationService,
     DataViewLayoutOptions

    ]
})
export class AdminModule { }
