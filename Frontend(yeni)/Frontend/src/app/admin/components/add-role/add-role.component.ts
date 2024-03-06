import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { BasePagination } from 'src/app/core/model/base-pagination';
import { CreateRolePermissionsDto } from 'src/app/model/createRolePermissions';
import { PermissionDto } from 'src/app/model/permission';
import { RolePermissionDto } from 'src/app/model/rolePermission';
import { RolePermissionFilter } from 'src/app/model/rolePermissionFilter';
import { RoleDto } from 'src/app/model/roles';
import { PermissionService } from 'src/app/services/permission.service';
import { RolePermissionService } from 'src/app/services/role-permission.service';
import { RolesService } from 'src/app/services/roles.service';

@Component({
  selector: 'app-add-role',
  templateUrl: './add-role.component.html',
  styleUrls: ['./add-role.component.scss']
})
export class AddRoleComponent extends BasePagination<RolePermissionFilter,RolePermissionDto> implements OnInit {

 rolePermissions:RolePermissionDto=new RolePermissionDto();
 rolePermission:RolePermissionDto[]=[];
 rolePermissionDialog?: boolean;


 isReadOnly: boolean = true; // var sayılan olarak readonly olmasını istediğimiz alanı seçtik
 deleteRolePermissionDialog = false;
 updateRolePermissionDialog?:boolean;
 submitted?: boolean;
 deleteRoleDialog = false;
 updateRoleDialog=false;
 isFilter: boolean = false;

 permissions:PermissionDto[]=[];
 permission:PermissionDto=new PermissionDto();
 permissionDialog?:boolean;
 role: RoleDto=new RoleDto();
 roles: RoleDto[]=[];
 addRoleDialog?:boolean;

 updateRoleId: string | undefined;


  constructor( private rolePermissionService:RolePermissionService, private messageService:MessageService ,private confirmationService: ConfirmationService,private permissionservice:PermissionService,private roleservice:RolesService) {

    super(rolePermissionService, messageService);
    this.pFilter = new RolePermissionFilter ();
  }

  ngOnInit(): void {

    //this.permissions=[];
    this.loadPermissions();
    this.loadRoles()
    this.loadData(event);


  }



  openNew() {

    this.rolePermissions=new RolePermissionDto
    this.rolePermissions = {};
    this.submitted = false;
    this.rolePermissionDialog = true;  // Bu satırı ekleyin
    this.isReadOnly = false;



  }


 openUpdateRolePermission(rolePermissions:RolePermissionDto){
    this.rolePermissions={...rolePermissions}
    this.rolePermissionDialog=false;
    this.updateRolePermissionDialog = true;
    this.isReadOnly = false; // Ekleme yapılabilir modda
    //this.updateRoleId = id;
  }





 openNewAddRoles() {
    this.role = {};
    this.submitted = false;
    this.addRoleDialog = true;
    this .isReadOnly=false; // Ekleme yapılabilir modda
  }


 hideDialog() {
    this.rolePermissionDialog = false;
    this.updateRolePermissionDialog=false;
    this.submitted = false;
  }


 hideUpdateDialog() {
    this.updateRolePermissionDialog = false;
    this.submitted = false;
    this.updateRoleId = undefined;
  }



//   saveUpdateRolePermission() {
//     this.submitted = true;


//     if (this.rolePermissions.roleId && this.rolePermissions.permissionIds) {
//         if (this.rolePermissions.id) {

//       this.rolePermissionService.updateRolePermission(this.rolePermissions).subscribe(result => {
//         if (result.isSuccess) {
//           this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Rol ve Yetki Güncellemesi Başarılı .', life: 3000 });
//           this.loadData(event);
//         } else {
//           this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
//         }

//       });
//     }
//   }

//     this.updateRolePermissionDialog = false;
//     this.rolePermissions = {};
//   }



 saveRolePermission() {

    this.submitted = true;

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





//  deleteRolePermission(rolePermissions: RolePermissionDto){

//     this.confirmationService.confirm({
//         message: rolePermissions.roleName + ' isimli Role ve Yetkiyi  silmek istediğinize emin misiniz??',
//         header: 'Uyarı',
//         icon: 'pi pi-exclamation-triangle',
//         accept: () => {


//         this.rolePermissionService.deleteRolePermission(rolePermissions.id).subscribe(result=>{
//             if (result.isSuccess){
//                 console.log();

//             this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Rol ve Yetki Silme Başarılı.', life: 3000 });

//             this.loadData(event);
//             }
//             else
//             this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
//         })
//             this.rolePermissions = {};
//         }
//     });
//   }

  openEditRolePermissions(rolePermissions: RolePermissionDto) {
    this.rolePermissions = { ...rolePermissions };
    this.loadPermissions(); // Yetki listesini yükle
    this.updateRolePermissionDialog = true;
  }


 loadPermissions(){



    this.permissionservice.getPermissions(this.permission).subscribe((response)=>{

        console.log('Permissions Result:', response);
        this.permissions = response.data;
        // this.permission = result.data;

       //console.log(this.expenseCategories);

     });
  }



 loadRoles() {

    this.roleservice.getRoles(this.role).subscribe((result) => {
      console.log('Roles Result:', result);
      this.roles = result.data;
    });
  }


 saveRoles() {
    this.submitted = true;


    // ExpenseTypeDto örneği oluşturuluyor ve name özelliğine değer atanıyor
    this.role = new RoleDto();
    this.role.name = this.rolePermissions.roleName; // Örnek olarak bir isim atanıyor, gerçek senaryoda kullanıcı girişi veya başka bir kaynaktan alınabilir.

    if (this.role && this.role.name) {
      this.roleservice.createRoles(this.role).subscribe(result => {
        if (result.isSuccess) {
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: 'Rol başarıyla eklendi.', life: 3000 });
          this.loadRoles(); // Yeni bir tür ekledikten sonra harcama türlerini tekrar yükle.
        } else {
          this.messageService.add({ severity: 'error', summary: 'Hata', detail: result.errors[0], life: 3000 });
        }
      });

      this.addRoleDialog = false;
      this.role = new RoleDto();
    }
  }

}
