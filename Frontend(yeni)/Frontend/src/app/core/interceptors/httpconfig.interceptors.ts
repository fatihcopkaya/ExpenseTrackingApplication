import { Injectable } from '@angular/core'
import {
    HttpInterceptor,
    HttpRequest,
    HttpHandler,
    HttpEvent,
    HttpErrorResponse
} from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable, throwError, catchError, switchMap, EMPTY} from 'rxjs';
import { AuthService } from '../services/auth.service';
import { MessageService } from "primeng/api";

@Injectable()
export class HttpConfigInterceptor implements HttpInterceptor
{
    constructor(private authService: AuthService,
                private router: Router,
                private messageService: MessageService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        let authReq = request;
        
        var accessToken = localStorage.getItem('accessToken');
        if (!authReq.url.includes('connect/token') && accessToken) {
            authReq = this.addTokenHeader(request, accessToken);
        }

        return next.handle(authReq).pipe(catchError((err: HttpErrorResponse) => {
            if (err.status === 401) {
                localStorage.removeItem('accessToken');
                localStorage.removeItem('refresh-token');
                localStorage.removeItem('username');
                this.router.navigate(['/']);
            }
            else if(err.status === 403)
            {
             this.messageService.add({ severity: 'error',  detail: 'Yetkisiz işlem', life: 3000 });
            }
            else
            {
                this.messageService.add({ severity: 'error', detail: 'İşlemleminiz başarılı bir şekilde gerçekleşemedi. Lütfen daha sonra tekrar deneyiniz.', life: 3000 });
            }
            return throwError(err);
          }));
    }

    private addTokenHeader(request: HttpRequest<any>, accessToken: string) {
        return request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + accessToken) });
    }

    // private handle401Error(request: HttpRequest<any>, next: HttpHandler)  {
    //     return this.authService.silentSignIn().pipe(
    //          switchMap(response => {
    //              localStorage.setItem('access-token',response.access_token);
    //              localStorage.setItem('refresh-token',response.refresh_token);

    //              return next.handle(this.addTokenHeader(request, response.access_token));
    //          })
    //     );
    // }
//
}
