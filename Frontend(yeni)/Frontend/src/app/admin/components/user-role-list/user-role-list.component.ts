// import { Component, OnInit } from '@angular/core';
// import { BasePagination } from 'src/app/core/model/base-pagination';
// import { MessageService } from 'primeng/api';
// import { ConfirmationService } from 'primeng/api';
// import { UserRoleDto } from 'src/app/model/userRole';
// import { UserRoleFilterDto } from 'src/app/model/userRoleFilter';
// import { UserRoleService } from 'src/app/services/user-role.service';
// import { UserInformationDTO } from 'src/app/model/userInformation';
// import { RoleDto } from 'src/app/model/roles';
// import { UserInformationService } from 'src/app/services/user-information.service';
// import { RoleService } from 'src/app/services/role.service';


// @Component({
//   selector: 'app-user-role-list',
//   templateUrl: './user-role-list.component.html',
//   styleUrls: ['./user-role-list.component.scss']
// })
// export class UserRoleListComponent extends BasePagination<UserRoleFilterDto, UserRoleDto>  implements OnInit  {

//  userRole:UserRoleDto=new UserRoleDto();
//  isReadOnly: boolean = true;
//  submitted?: boolean;
//  loading: boolean = false;
//  isFilter: boolean = false;
//  isText:boolean=false;
//  selectedUserInfo: UserInformationDTO | null = null;
//  selectedRole: RoleDto | null = null;
//  roles: RoleDto[] = [];
//  role:RoleDto=new RoleDto();
//  userInfos: UserInformationDTO[] = [];
//  userInfo:UserInformationDTO=new UserInformationDTO();
//  UserRoleDialog?: boolean;
//  UpdateUserRoleDialog?: boolean;



//   constructor(private messageService: MessageService,private confirmationService: ConfirmationService,public userRoleService:UserRoleService,public userInformationService: UserInformationService,public roleService:RoleService) {
//     super(userRoleService, messageService);
//     this.pFilter = new UserRoleFilterDto();
//   }

//   ngOnInit(): void {
//     //this.expenseCategories = [];

//     this.loadData(event);
//     this.loadUserInfos();
//     this.loadRoleInfos();

//   }

//   override loadData(event: any): void {

//     this.pFilter = new UserRoleFilterDto();
//     super.loadData(event);
//   }

//   loadUserInfos(){

//     this.userInformationService.getUserNameForPayment(this.userInfo).subscribe((response)=>{

//        this.userInfos=response.data;



//      });
//   }
//   loadRoleInfos(){

//     this.roleService.getRoleInfo(this.role).subscribe((response)=>{


//       this.roles=response.data;


//     });
//   }

//   openNew() {
//     this.userRole = {};
//     this.submitted = false;
//     this.UserRoleDialog = true;
//     this.isReadOnly=false; // Ekleme yapılabilir modda
//   }
//   openUpdateUserRole(id: string){
//     this.userRole.id = id;
//     this.UpdateUserRoleDialog = true;
//   }
//   hideDialog() {
//     this.UserRoleDialog = false;
//     this.UpdateUserRoleDialog=false;
//     this.submitted = false;
//   }

//   saveUserRole() {
//       this.submitted = true;
//       //debugger;

//       if (this.userRole.appUserId && this.userRole.roleId) {
//       if (this.userRole.id) {
//           this.userRoleService.updateUserRole(this.userRole).subscribe(result=>{
//           if (result.isSuccess){

//           this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Kullanıcı Rolü Güncelleme Talebi İletildi.', life: 3000 });
//           this.loadData(event);
//           }
//           else
//           this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
//           })

//       }

//       else {

//           this.userRoleService.createUserRole(this.userRole).subscribe(result=>{
//           if (result.isSuccess){
//           this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Kullanıcı Rolü Ekleme Talebi İletildi.', life: 3000 });

//           this.loadData(event);

//           }
//           else
//           this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
//           })
//       }

//       this.UserRoleDialog = false;
//       this.userRole = {};
//     }
//   }



//   updateUserRole(userRole: UserRoleDto) {
//       this.userRole = { ...userRole }; // userRole içeriğini doğrudan this.userRole'e atar
//       this.UserRoleDialog = true;
//       this.UpdateUserRoleDialog = true;
//       this.isReadOnly = true; // Düzenleme modunda
//   }



//   deleteUserRole(userRole: UserRoleDto){

//     this.confirmationService.confirm({
//         message: userRole.appUserFirstName + ' isimli kullanıcı grubunu silmek istediğinize emin misiniz??',
//         header: 'Uyarı',
//         icon: 'pi pi-exclamation-triangle',
//         accept: () => {


//         this.userRoleService.deleteUserRole(userRole.id).subscribe(result=>{

//             if (result.isSuccess){
//                 console.log();

//             this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Kullanıcı Rolü Silme Talebi İletildi.', life: 3000 });

//             this.loadData(event);
//             }
//             else
//             this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
//         })
//             this.userRole = {};
//         }
//     });
//   }






// }
