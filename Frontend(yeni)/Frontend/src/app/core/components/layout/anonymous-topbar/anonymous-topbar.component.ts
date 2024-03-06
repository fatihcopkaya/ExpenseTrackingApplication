import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { LayoutService } from 'src/app/core/services/app.layout.service';



@Component({
  selector: 'anonymous-topbar',
  templateUrl: './anonymous-topbar.component.html'
})
export class AnonymousTopbarComponent implements OnInit {

  items!: MenuItem[];

  @ViewChild('menubutton') menuButton!: ElementRef;

  @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;

  @ViewChild('topbarmenu') menu!: ElementRef;

  constructor(public layoutService: LayoutService) { }

  ngOnInit(): void {
  }

}
