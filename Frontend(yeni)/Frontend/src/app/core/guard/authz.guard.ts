import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router } from '@angular/router';
import { Observable,of } from 'rxjs';
import { catchError, map} from 'rxjs/operators';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthzGuard implements CanActivate {

  constructor(private router:Router, private authService:AuthService) {}

  canActivate(route: ActivatedRouteSnapshot) : Observable<boolean>{ 
    if(route.data['expectedPermission'] === undefined)
    {
      return of(true);
    } 
    
    const expectedPermission = route.data['expectedPermission'];
    return this.authService.checkPermission(expectedPermission)
                           .pipe(map((res) => { 
                              if(res.isSuccess){
                               return true;
                             }
                            else 
                             {
                                this.router.navigate(['/admin']);
                                return false;
                            }
                           }) 
                           ,catchError(() => { 
                               this.router.navigate(['/admin']);/// bizde buralar değişebilir ilyas baiye sorulacak.
                              return of(false);
                           })); 
  } 
}
