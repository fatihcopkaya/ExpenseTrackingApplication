import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { TranslateService } from '@ngx-translate/core';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {

    lang: string = "tr";

    subscription: Subscription;


    constructor(private primengConfig: PrimeNGConfig,private translateService: TranslateService ) {
        translateService.addLangs(['en', 'tr']);
        translateService.setDefaultLang('tr');

        const browserLang = translateService.getBrowserLang();
         let lang = browserLang?.match(/en|tr/) ? browserLang : 'tr';
         this.changeLang(lang);

        this.subscription = this.translateService.stream('primeng').subscribe(data => {
            this.primengConfig.setTranslation(data);
        });
     }

     changeLang(lang: string) {
        // this.translateService.use(lang);
     }

     ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }

    ngOnInit() {}
}
