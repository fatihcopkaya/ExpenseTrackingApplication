import { Component, OnInit, } from '@angular/core';
import { MessageService } from 'primeng/api';


@Component({
    templateUrl: './dashboard.component.html',
    styleUrls: ['./dashboard.componenent.scss'],
})
export class AdminDashboardComponent implements OnInit {
   
    constructor( private MessageService: MessageService
       
    ) {
        
    }

    ngOnInit() {

       

    }
  
}
