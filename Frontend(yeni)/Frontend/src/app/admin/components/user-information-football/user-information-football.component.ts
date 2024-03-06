import { Component, OnInit } from '@angular/core';
import { UserInformationService } from 'src/app/services/user-information.service';
import { UserInformationDTO } from 'src/app/model/userInformation';
import { BasePagination } from 'src/app/core/model/base-pagination';
import { MessageService } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { UserInformationFilter } from 'src/app/model/userInformationFilter';
import { ExpenseCategoryService } from 'src/app/services/expensecategory.service';
import { expensecategoryDto } from 'src/app/model/expensecategory';

@Component({
  selector: 'user-information-football',
  templateUrl: './user-information-football.component.html',
  styleUrls: ['./user-information-football.component.scss']
})
export class UserInformationFootballComponent  extends BasePagination<UserInformationFilter, UserInformationDTO>  implements OnInit {

    users: UserInformationDTO = new UserInformationDTO();
    isReadOnly: boolean = true; // var sayılan olarak readonly olmasını istediğimiz alanı seçtik

    expenseCategories:expensecategoryDto[]=[];

   UserInformationDialog?: boolean;
   submitted?: boolean;
   deleteUserInformationDialog = false;
   updateUserInformationDialog=false;
   isFilter: boolean = false;

   loading: boolean = false;

   type: string ="password";
   isText:boolean=false;
   eyeIcon:string="pi-eye-slash";


   authoritiesDialogVisible?: boolean = false;
   selectedUserForAuthorities?: UserInformationDTO;
   selectedUserRoles: string[] = [];

//    selectedSuperAdminRole: boolean = false;
//    selectedAdminRole: boolean = false;
//    selectedUserRole: boolean = false;

  constructor(public userInformationService: UserInformationService,  private messageService: MessageService,private confirmationService: ConfirmationService,private expensecategoryService:ExpenseCategoryService) {

    super(userInformationService, messageService);
    this.pFilter = new UserInformationFilter ();
   }

  ngOnInit(): void {

    this.expenseCategories = [];
    this.loadData(event);
    this.loadExpenseCategories();
    // this.selectedAdminRole = false;
    // this.selectedUserRole = false;
    // this.selectedSuperAdminRole = false;
  }






    // hideAuthoritiesDialog() {
    //     this.authoritiesDialogVisible = false;
    // }


    // saveAuthoritiesInfo() {
    //     // Yetkileri kaydetmek için gerekli işlemleri burada gerçekleştirin.
    //     // Örneğin, this.selectedUserForAuthorities üzerinden seçilen kullanıcının yetkilerini kaydedebilirsiniz.
    //     // İşlemleri tamamladıktan sonra authoritiesDialogVisible özelliğini false yapabilirsiniz.
    //     console.log('Seçilen Roller:', this.selectedUserRoles);
    //     this.authoritiesDialogVisible = false;
    // }

  override loadData(event: any): void {
    //    this.pFilter = new UserInformationFilter();
    this.pFilter = {
          expenseCategoryType: 2, // 1, ExpenseCategoryType 1'e (örneğin, Kahve) eşit olacak şekilde filtreleme yapıyor.
          // Diğer filtre özellikleri
        };
       super.loadData(event);
    }


    loadExpenseCategories(){

          this.expensecategoryService.getExpenseCategories().subscribe((result)=>{
              this.expenseCategories=result.data;
              console.log(this.expenseCategories);

          });
          };

    openNew() {
        this.users = {};
        this.submitted = false;
        this.UserInformationDialog = true;
        this .isReadOnly=false; // Ekleme yapılabilir modda


    }

    openUpdateUserInformation(id: string){
        this.users.id = id;
        this.updateUserInformationDialog = true;
    }


    hideDialog() {
        this.UserInformationDialog = false;
        this.updateUserInformationDialog=false;
        this.submitted = false;
    }

    hideShowPass(){
        this.isText=!this.isText;
        this.isText ? this.eyeIcon="pi-eye": this.eyeIcon="pi-eye-slash";
         this.isText ? this.type="text":this.type="password";

    }

    saveUserInfo() {
        this.submitted = true;

        if (this.users.firstName && this.users.lastName) {
        if (this.users.id) {
            this.userInformationService.updateUserInfo(this.users).subscribe(result=>{
            if (result.isSuccess){

            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Kullanıcı  Güncelleme Başarılı.', life: 3000 });
            this.loadData(event);
            }
            else
            this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
            })

        }

        else {

            this.userInformationService.createUserInfo(this.users).subscribe(result=>{
            if (result.isSuccess){
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Kullanıcı  Ekleme Başarılı.', life: 3000 });

            this.loadData(event);

            }
            else
            this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
            })
        }

        this.UserInformationDialog = false;
        this.users = {};
      }
    }

    updateUserInfo(users: UserInformationDTO) {

        this.users = {...users};
        this.UserInformationDialog = true;
        this.updateUserInformationDialog=true;

        this.isReadOnly=true;// Düzenleme modunda
    }




    deleteUserInfo(users: UserInformationDTO){

        this.confirmationService.confirm({
            message: users.firstName + ' isimli kullanıcıyı silmek istediğinize emin misiniz??',
            header: 'Uyarı',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {


            this.userInformationService.deleteUserInfo(users.id).subscribe(result=>{
                if (result.isSuccess){
                    console.log();

                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Kullanıcı  Silme Başarılı.', life: 3000 });

                this.loadData(event);
                }
                else
                this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
            })
                this.users = {};
            }
        });
    }

}
