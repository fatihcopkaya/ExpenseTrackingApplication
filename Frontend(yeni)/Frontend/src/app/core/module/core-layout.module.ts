import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { BrowserModule } from "@angular/platform-browser";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { RouterModule } from "@angular/router";
import { MenubarModule } from "primeng/menubar";
import { SidebarModule } from "primeng/sidebar";
import { SharedModule } from "src/app/shared/shared.module";
import { AnonymousTopbarComponent } from "../components/layout/anonymous-topbar/anonymous-topbar.component";
import { AppFooterComponent } from "../components/layout/footer/app.footer.component";
import { AppMenuComponent } from "../components/layout/menu/app.menu.component";
import { AppMenuitemComponent } from "../components/layout/menu/app.menuitem.component";
import { AppSidebarComponent } from "../components/layout/sidebar/app.sidebar.component";
import { AppTopBarComponent } from "../components/layout/topbar/app.topbar.component";
import { AppConfigModule } from "../config/config.module";


@NgModule({
    declarations: [
        AppTopBarComponent,
        AppSidebarComponent,
        AppFooterComponent,
        AppMenuComponent,
        AppMenuitemComponent,
        AnonymousTopbarComponent,

    ],
    imports: [
        SidebarModule,
        BrowserModule,
        MenubarModule,
        BrowserAnimationsModule,
        RouterModule,
        SharedModule,
        AppConfigModule,
        CommonModule
    ],
    exports: [
        AppTopBarComponent,
        AppSidebarComponent,
        AppFooterComponent,
        AppMenuComponent,
        AppMenuitemComponent,
        AppConfigModule,
        AnonymousTopbarComponent,
        BrowserModule
    ]
    })
export class CoreLayoutModule { }
