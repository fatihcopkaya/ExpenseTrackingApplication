import { Component } from '@angular/core';
import { LayoutService } from 'src/app/core/services/app.layout.service';


@Component({
    selector: 'app-footer',
    templateUrl: './app.footer.component.html'
})
export class AppFooterComponent {
    constructor(public layoutService: LayoutService) { }
}
