import { NgModule } from '@angular/core';
import { HashLocationStrategy, LocationStrategy } from '@angular/common';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { MessageService } from 'primeng/api';
import { AnonymousLayoutModule } from './anonymous/layout/layout.module'
import { AdminLayoutModule } from './admin/layout/layout.module';
import { CoreModule } from './core/module/core.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/shared.module';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';



export function HttpLoaderFactory(httpClient: HttpClient) {
    return new TranslateHttpLoader(httpClient);
}

@NgModule({
    declarations: [
        AppComponent,


    ],
    imports: [
        AppRoutingModule,
        BrowserModule,
        RouterModule,
        AnonymousLayoutModule,
        AdminLayoutModule,
        CoreModule,
        SharedModule,
        BrowserAnimationsModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: HttpLoaderFactory,
                deps: [HttpClient]
            }

        }),
        TableModule,
        DropdownModule
    ],
    providers: [
            { provide: LocationStrategy, useClass: HashLocationStrategy },
            MessageService
    ],
    bootstrap: [AppComponent]})
export class AppModule { }
