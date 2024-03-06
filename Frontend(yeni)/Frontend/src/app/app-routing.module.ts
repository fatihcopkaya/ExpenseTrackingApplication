import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { AnonymousLayoutComponent } from './anonymous/layout/layout.component'; 
import { AdminLayoutComponent } from './admin/layout/layout.component';
import { NotfoundComponent } from './core/components/notfound/notfound.component';

@NgModule({
    imports: [
        RouterModule.forRoot([
            { path: '', component:AnonymousLayoutComponent,
                children: [
                    { path: '', loadChildren: () => import('./anonymous/anonymous.module').then(m => m.AnonymousModule) }
                ]
            },
            { path: 'admin', component: AdminLayoutComponent,
                children: [
                    { path: '', loadChildren: () => import('./admin/admin.module').then(m => m.AdminModule) },
                ]
            },
            { path: 'notfound', component: NotfoundComponent },
            { path: '**', redirectTo: '/notfound' },
        ], { scrollPositionRestoration: 'enabled', anchorScrolling: 'enabled', onSameUrlNavigation: 'reload' })
    ],
    exports: [RouterModule]
})
export class AppRoutingModule {
}
