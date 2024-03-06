import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { MenuChangeEvent } from '../model/menuchangeevent';


@Injectable({
    providedIn: 'root'
})
export class MenuService {


    private menuSource = new Subject<MenuChangeEvent>();
    private resetSource = new Subject();

    menuSource$ = this.menuSource.asObservable();
    resetSource$ = this.resetSource.asObservable();

    /**
     *
     */
    constructor(private http: HttpClient) {
      
        
    }
    onMenuStateChange(event: MenuChangeEvent) {
        this.menuSource.next(event);
        
    }

    reset() {
        this.resetSource.next(true);
    }

    getMenus(){
        return this.http.get<any[]>('assets/demo/data/menuitemsv1.json');
    }
}
