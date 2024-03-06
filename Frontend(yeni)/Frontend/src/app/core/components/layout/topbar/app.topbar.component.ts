import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { MenubarModule } from 'primeng/menubar';
import { MenuItem } from 'primeng/api';
import { LayoutService } from "../../../services/app.layout.service";
//import { ApprovalCountService } from 'src/app/core/services/approval.count.service';
import { NameInfoDto } from 'src/app/core/model/topbar-name';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';
import { AppTopbarService } from 'src/app/core/services/app-topbar.service';

@Component({
    selector: 'app-topbar',
    templateUrl: './app.topbar.component.html'
})
export class AppTopBarComponent {

    items!: MenuItem[];
    pendingApprovalsCount:number=0;
    // nameInfo:NameInfoDto=new NameInfoDto();
    // nameInfos:NameInfoDto[]=[];
    @ViewChild('menubutton') menuButton!: ElementRef;

    @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;

    @ViewChild('topbarmenu') menu!: ElementRef;


    constructor(
        public layoutService: LayoutService,
        private router: Router,
        //private approvalCountService: ApprovalCountService,
        public authService: AuthService/*,
        public apptopbarService:AppTopbarService,*/
     ) { }
    ngOnInit() {
        //this.loadPendingApprovalsCount();
        this.getUsername();

    }

    getUsername(){
        const name= localStorage.getItem('firstName') + " " + localStorage.getItem('lastName');
        return name;
       
    
    }

    logout() {
        this.authService.signOut();
    }

    formatDate(date = new Date()) {
        const year = date.toLocaleString('default', { year: 'numeric' });
        const month = date.toLocaleString('default', { month: '2-digit' });
        const day = date.toLocaleString('default', { day: '2-digit' });

        return [year, month, day].join('-');
    }
    // navigateToApprovalList() {
    //     this.router.navigate(['admin/approvallist']);
    //}
    // loadPendingApprovalsCount() {
    //     this.approvalCountService.getPendingApprovalsCount().subscribe(result => {
    //         if(result.isSuccess){
    //             this.pendingApprovalsCount = result.data.count;
    //         }
    //       });
    // }
}
