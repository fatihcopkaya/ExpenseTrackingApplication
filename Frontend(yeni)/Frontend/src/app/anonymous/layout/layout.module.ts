import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AnonymousLayoutComponent } from "./layout.component";
import { CoreLayoutModule } from 'src/app/core/module/core-layout.module';
import { RouterModule } from '@angular/router';
import { MenubarModule } from 'primeng/menubar';
import { SharedModule } from 'src/app/shared/shared.module';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
    declarations: [
        AnonymousLayoutComponent,
    ],
    imports: [
        CoreLayoutModule,
        FormsModule,
        HttpClientModule,
        SharedModule,
        MenubarModule,
        RouterModule,
        ReactiveFormsModule
    ],
    exports: [AnonymousLayoutComponent]
})
export class AnonymousLayoutModule { }
