
import { CommonModule } from "@angular/common";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { NgModule } from "@angular/core";
import { Router, RouterModule } from "@angular/router";
import { MessageService } from "primeng/api";
import { SharedModule } from "../../shared/shared.module";
import { NotfoundComponent } from "../components/notfound/notfound.component";
import { HttpConfigInterceptor } from "../interceptors/httpconfig.interceptors";
import { AuthService } from "../services/auth.service";


//Core ve CoreLayout dosyalarının ayrı olması sebepleri browser module ile common module aynı yerde import edildiği için hata alınmaktadır. 
//Bu modulü genellikle sistem için ortak işlemler eklenebilir.
@NgModule({
    declarations: [
        NotfoundComponent
    ],
    imports: [
        CommonModule,
        HttpClientModule,
        RouterModule,
        SharedModule
    ],
    exports: [
        NotfoundComponent,
      
    ],
    providers : [     
     { provide: HTTP_INTERCEPTORS, useClass: HttpConfigInterceptor, multi: true, deps: [AuthService, Router, MessageService]}]
    }) 
export class CoreModule { }
