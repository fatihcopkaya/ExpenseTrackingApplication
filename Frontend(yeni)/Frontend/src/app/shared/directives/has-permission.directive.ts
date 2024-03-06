import { Directive, Input, TemplateRef, ViewContainerRef } from '@angular/core';
import { AuthService } from 'src/app/core/services/auth.service';


@Directive({ selector: '[hasPermission]'})
export class HasPermissionDirective {
  constructor(
    private templateRef: TemplateRef<any>,
    private viewContainer: ViewContainerRef,
    private authService: AuthService) { }

  @Input() set hasPermission(permission: string) {
    if(permission){
      this.authService.checkPermission(permission)
                   .subscribe(response =>
                   {
                        if(response.isSuccess)
                        {
                           this.viewContainer.createEmbeddedView(this.templateRef);
                        }
                        else
                        {
                           this.viewContainer.clear();
                        }
                   },
                   () => {
                        this.viewContainer.clear();
                   });
    }
    else{
      this.viewContainer.createEmbeddedView(this.templateRef);
    }

  }
}

export interface CheckPermissionResponse {
    isSuccess : boolean;
    errors : string[];//burayı da sor
}
