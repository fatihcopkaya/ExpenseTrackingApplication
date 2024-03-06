import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { BasePagination } from 'src/app/core/model/base-pagination';
import { ListPermissionByRolesDto } from 'src/app/model/listPermissionsByRoleId';
import { PermissionDto } from 'src/app/model/permission';
import { RoleFilter } from 'src/app/model/roleFilter';
import { RolePermissionDto } from 'src/app/model/rolePermission';
import { RolePermissionFilter } from 'src/app/model/rolePermissionFilter';
import { RoleDto } from 'src/app/model/roles';
import { PermissionService } from 'src/app/services/permission.service';
import { RolePermissionService } from 'src/app/services/role-permission.service';
import { RoleService } from 'src/app/services/role.service';






@Component({
  selector: 'role-permissions',
  templateUrl: './role-permissions.component.html',
  styleUrls: ['./role-permissions.component.scss']
})
export class RolePermissionsComponent extends BasePagination<RoleFilter,RoleDto> implements OnInit {

  permissionns: any[] = [];
  filteredPermissionByRoles: any[] = [];
  filterText: string = '';

  roles: RoleDto[] = [];
  selectedRoleId: string | null = null;
  permissions: PermissionDto[] = [];
  permission:PermissionDto=new PermissionDto()
  role: RoleDto=new RoleDto();


  rolePermissions:RolePermissionDto=new RolePermissionDto();
 rolePermission:RolePermissionDto[]=[];
 permissionByRoles: ListPermissionByRolesDto[] = [];
 permissionByRole:ListPermissionByRolesDto=new ListPermissionByRolesDto();


 submitted?: boolean;
 showPopup: boolean = false; // Yeni eklenen özellik
 loading: boolean = false;
 isReadOnly: boolean = true; // var sayılan olarak readonly olmasını istediğimiz alanı seçtik
 updateRoleId: string | undefined;
 permissionsForRole: any[] = [];

 //slider
 sliderValue: number = 0;

//Dialogs
 rolePermissionDialog?: boolean;
 updateRolePermissionDialog?:boolean;
 deleteRolePermissionDialog = false;
 roleDialog?:boolean;
 updateRoleDialog?:boolean;
 deleteRoleDialog?:false;
 addRoleDialog?:boolean;

 selectedPermissions: string[] = [];
 displayDialog = false;



  constructor(private roleService: RoleService, private permissionService: PermissionService,private confirmationService: ConfirmationService,private rolePermissionService:RolePermissionService,private messageService:MessageService, private router: Router,) {
    super(roleService, messageService);
    // this.pFilter = new RolePermissionFilter();
    this.pFilter=new RoleFilter();
   }

  ngOnInit(): void {

     this.loadRoles();
     this.loadPermissions();
     //this.loadData(event);
     //this.getPermissionsByRoleId();

     //this.loadRolePermissions();
     console.log(this.permissions.map(permission => permission.id));


  }



  openNew() {

    debugger;

    this.rolePermissions=new RolePermissionDto
    this.rolePermissions = {};
    this.submitted = false;
    this.rolePermissionDialog = true;  // Bu satırı ekleyin
    this.isReadOnly = false;
    //this.cdr.detectChanges();


  }


  openDelete(){

    debugger;

    this.rolePermissions=new RolePermissionDto
    //this.rolePermissions = {};
   // this.rolePermissionsForDelete={roleId:null,permissionIds:[]}
    this.submitted = false;
    this.deleteRolePermissionDialog = true;  // Bu satırı ekleyin
    this.isReadOnly = false;
  }


  openNewAddRoles() {

    debugger;
    this.role = {};
    this.submitted = false;
    this.addRoleDialog = true;
    this .isReadOnly=false; // Ekleme yapılabilir modda
  }

  override loadData(event: any): void {

    this.pFilter = new RoleFilter();
    super.loadData(event);
 }



 hideInfoDialog() {
    this.updateRoleDialog = false;
    this.submitted = false;
    this.updateRoleId = undefined;
    this.filterText = '';
  }

  hideDialog() {
    this.rolePermissionDialog = false;
    this.updateRolePermissionDialog=false;
    this.submitted = false;
  }

  getPermissionsByRoleId(roleId: string) {

    debugger;
    // Backend'den seçilen rol için yetki listesini al
    if (roleId !==null) {
      this.permissionService.getPermissionsByRoleId(roleId).subscribe(
        (result) => {
          // Servis tarafından gelen yetkileri result değişkeninde kullanabilirsiniz
          console.log(result.data);
          this.permissionByRoles = result.data; // Yetkileri saklayın, eğer result.data undefined ise boş bir dizi atayın
        },
        (error) => {
          // Hata durumunda işlemleri gerçekleştirin
          console.error(error);
          // Hata durumunda this.permissions'ı boş bir dizi olarak ayarlayabilirsiniz.
          this.permissions = [];
        }
      );
    } else {
      console.error("Role ID is undefined or null.");
      // Role ID undefined veya null ise this.permissions'ı boş bir dizi olarak ayarlayabilirsiniz.
      this.permissionByRoles = [];
    }
  }


//   loadRolePermissions() {


//     this.rolePermissionService.getRolePermissions(this.rolePermissions).subscribe(
//       (result) => {
//         console.log(result);
//         this.processPermissions(result); // Veriyi işleyen bir metod çağırabilirsiniz
//       },
//       (error) => {
//         console.error(error);
//         this.permissions = []; // Hata durumunda this.permissions'ı boş bir dizi olarak ayarlayabilirsiniz.
//       }
//     );
//   }

  loadRoles() {

    this.roleService.getRoleInfo(this.role).subscribe((result) => {
      console.log('Roles Result:', result);
      this.roles = result.data;
    });
  }

  loadPermissions(){


    debugger;
    this.permissionService.getPermissions(this.permission).subscribe((response)=>{

        console.log('Permissions Result:', response);
        this.permissions = response.data;
        // this.permission = result.data;

       console.log(response.data);

     });
  }


  saveRolePermission() {

    this.submitted = true;
    debugger;

    if (this.rolePermissions.roleId && this.rolePermissions.permissionIds ) {

        this.rolePermissionService.createRolePermission(this.rolePermissions).subscribe(result=>{

        if (result.isSuccess)
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Role ve Yetki Eklemesi Başarılı .', life: 3000 });
        else
        this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
        })
    //}

    this.rolePermissionDialog = false;
    this.rolePermissions = {};
  }
 }


  processPermissions(result: any) {
    // Veriyi işleyin, örneğin bir değişkene atayabilir veya başka işlemler gerçekleştirebilirsiniz
    this.permissions = result.data;
  }







  openInfoRolePermission(role: RoleDto) {

    debugger;
    // Pop-up açıldığında rol ID'sini al
    // Burada gerekli servis çağrısını yapabilirsiniz
    //this.rolePermissions = {};
  //  this.role = { ...role };
   // this.permissions = [];
    this.getPermissionsByRoleId(role.id!);
    this.submitted = false;

    this.loadRoles();


    // Pop-up'ı görünür yap
    this.updateRoleDialog = true;
  }






  confirmDeleteRolePermission(rolePermissions: RolePermissionDto): void {
    this.confirmationService.confirm({
        message: rolePermissions.roleName + ' isimli Rol ve Yetkiyi silmek istediğinize emin misiniz??',
        header: 'Uyarı',
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            // Evet'e basıldığında gerçekleşecek işlemleri burada çağırabilirsiniz
            this.deleteRolePermission(rolePermissions);
        },

    });
}


 deleteRolePermission(rolePermissions: RolePermissionDto): void {
  debugger;
  console.log('Role Permissions:', rolePermissions);
  if (rolePermissions.roleId && rolePermissions.permissionIds && rolePermissions.permissionIds.length > 0) {
    this.confirmationService.confirm({
      message:' Seçili Rol ve Yetkiyi silmek istediğinize emin misiniz?',
      header: 'Uyarı',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.rolePermissionService.deleteRolePermission(rolePermissions.roleId??'', rolePermissions.permissionIds??[]).subscribe(result => {
          if (result.isSuccess) {
            console.log();

            this.messageService.add({
              severity: 'success',
              summary: 'Successful',
              detail: 'Rol ve Yetki Silme Başarılı.',
              life: 3000
            });

            this.loadData(event);
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: result.errors[0],
              life: 3000
            });
          }
        });

        this.rolePermissions = {}; // Eğer bu satırın amacı rolePermissions'ı temizlemekse, bu kısmı uygun şekilde güncelleyin.

        this.deleteRolePermissionDialog = false; // Dialog'u kapat
      }
    });
  }
}



deleteRoles(roles: RoleDto){
    debugger;

        this.confirmationService.confirm({
            message: roles.name + ' isimli Rolü  silmek istediğinize emin misiniz?',
            header: 'Uyarı',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {


            this.roleService.deleteRoleInfo(roles.id).subscribe(result=>{
                if (result.isSuccess){
                    console.log();

                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Rol  Silme Başarılı.', life: 3000 });
                 // Sayfayı yenile
                 setTimeout(() => {
                    // Sayfayı yenile
                    this.router.routeReuseStrategy.shouldReuseRoute = () => false;
                    const currentUrl = this.router.url + '?';
                    this.router.navigateByUrl(currentUrl).then(() => {
                      this.router.navigate([this.router.url]);
                    });
                    this.loadData(event);
                  }, 700); // Mesajın ekranda kalma süresinden (life) biraz daha uzun bir süre bekleyebilirsiniz
                this.loadData(event);
                }
                else
                this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
            })
                this.role = {};
                this.deleteRoleDialog=false;
            }
        });
      }


  saveRoles() {
    this.submitted = true;



    this.role = new RoleDto();
    this.role.name = this.rolePermissions.roleName; // Örnek olarak bir isim atanıyor, gerçek senaryoda kullanıcı girişi veya başka bir kaynaktan alınabilir.

    if (this.role && this.role.name) {
      this.roleService.createRoleInfo(this.role).subscribe(result => {
        if (result.isSuccess) {
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: 'Rol başarıyla eklendi.', life: 3000 });
          this.loadRoles();
        } else {
          this.messageService.add({ severity: 'error', summary: 'Hata', detail: result.errors[0], life: 3000 });
        }
      });

      this.addRoleDialog = false;
      this.role = new RoleDto();
    }
  }


  filter(value: string, column: string) {
    // backend'e sorgu yaparak filtreleme
    this.permissionService.getPermissionsByRoleId(this.role.id!).subscribe(
      (result) => {
        this.permissions = result;
      },
      (error) => {
        console.error('Error while filtering permissions:', error);
      }
    );
  }


}
