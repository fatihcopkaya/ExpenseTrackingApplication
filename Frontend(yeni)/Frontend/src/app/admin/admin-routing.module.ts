import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AdminDashboardComponent } from './components/dashboard/dashboard.component';
import { EmptyDemoComponent } from './components/empty/emptydemo.component';
import { AuthzGuard } from '../core/guard/authz.guard';

import { HomeComponent } from './components/home/home.component';
import { AdduserComponent } from './components/adduser/adduser.component';
import { PaymentsComponent } from './components/payments/payments.component';
import { ReportComponent } from './components/report/report.component';
import { SettingsComponent } from './components/settings/settings.component';
import { PasswordChangeComponent } from './components/password-change/password-change.component';
import { PaymentInformationsComponent } from './components/payment-informations/payment-informations.component';
import { ProfileComponent } from './components/profile/profile.component';
import { AddRoleComponent } from './components/add-role/add-role.component';
import { PaymentFootballComponent} from './components/payment-football/payment-football.component';
import { PaymentsCoffeeComponent } from './components/payments-coffee/payments-coffee.component';
import { UserinfomationCoffeeComponent } from './components/userinfomation-coffee/userinfomation-coffee.component';
import { UserInformationFootballComponent } from './components/user-information-football/user-information-football.component';
import { ExpenseInfoComponent } from './components/expense-info/expense-info.component';
// import { UserRoleListComponent } from './components/user-role-list/user-role-list.component';
import { ExpenseReportComponent } from './components/expense-report/expense-report.component';
import { RolePermissionsComponent } from './components/role-permissions/role-permissions.component';


@NgModule({
    imports: [RouterModule.forChild([
        {
            path: 'dashboard',
            component: AdminDashboardComponent,
        },

        { path: 'home',
         component: HomeComponent,
        //  canActivate: [AuthzGuard],
        //     data: {
        //         expectedRoles: 'Süper Admin,Admin,Kullanıcı'
        //     },
        },


        { path: 'add-user',
         component: AdduserComponent,
        //  canActivate: [AuthzGuard],
        //     data: {
        //         expectedPermission: 'Süper Admin,Admin,Kullanıcı'
        //     },
        },


        { path: 'userinfomation-coffee',
         component:UserinfomationCoffeeComponent,
        //  canActivate: [AuthzGuard],
        //     data: {
        //         expectedPermission: 'Süper Admin,Admin,Kullanıcı'
        //     },
        },

        { path: 'user-information-football',
        component:UserInformationFootballComponent,
        // canActivate: [AuthzGuard],
        //     data: {
        //         expectedPermission: 'Süper Admin,Admin,Kullanıcı'
        //     },
        },

        { path: 'payments',
        component: PaymentsComponent,
        // canActivate: [AuthzGuard],
        //     data: {
        //         expectedPermission: 'Süper Admin,Admin,Kullanıcı'
        //     },
       },

       { path: 'report',
       component: ReportComponent
       },


       { path: 'role-permissions',
       component: RolePermissionsComponent
       },

       { path: 'expense-report',
       component: ExpenseReportComponent
       },

       { path: 'payment-informations',
       component: PaymentInformationsComponent,
    //    canActivate: [AuthzGuard],
    //         data: {
    //             expectedPermission: 'Süper Admin,Admin,Kullanıcı'
    //         },
       },

       { path: 'expense-info',
       component: ExpenseInfoComponent,
    //    canActivate: [AuthzGuard],
    //         data: {
    //             expectedPermission: 'Süper Admin,Admin,Kullanıcı'
    //         },
       },

       { path: 'settings',
       component: SettingsComponent
       },

       { path: 'coffee-payment',
       component: PaymentsCoffeeComponent,
    //    canActivate: [AuthzGuard],
    //         data: {
    //             expectedPermission: 'Süper Admin,Admin,Kullanıcı'
    //         },
       },

       { path: 'football-payment',
       component: PaymentFootballComponent,
    //    canActivate: [AuthzGuard],
    //         data: {
    //             expectedPermission: 'Süper Admin,Admin,Kullanıcı'
    //         },
       },


       { path: 'password-change',
       component: PasswordChangeComponent
       },

       { path: 'profile',
       component: ProfileComponent
       },

    //    { path: 'user-role-list',
    //    component: UserRoleListComponent,
    // //    canActivate: [AuthzGuard],
    // //         data: {
    // //             expectedPermission: 'Süper Admin'
    // //         },
    //    },


       { path: 'add-role',
       component: AddRoleComponent,
    //    canActivate: [AuthzGuard],
    //         data: {
    //             expectedPermission: 'Süper Admin'
    //         },
       },



        {
            path: 'empty',
            component: EmptyDemoComponent
        },







    ])],
    exports: [RouterModule]
})
export class AdminRoutingModule { }
