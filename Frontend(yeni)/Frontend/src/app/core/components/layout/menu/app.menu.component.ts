import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from 'src/app/core/services/app.layout.service';
import { MenuService } from '../../../services/app.menu.service';


@Component({
    selector: 'app-menu',
    templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

    model: any[] = [];
    userpermission!: string;

    constructor(public layoutService: LayoutService,private menuService:MenuService) { }

    ngOnInit() {
        this.userpermission=localStorage.getItem("perm")!;
        this.menuService.getMenus().subscribe(data=>{          
            this.model=data;
          })
        
    }
}
