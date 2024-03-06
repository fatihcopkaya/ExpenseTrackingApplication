import { Component, OnInit } from '@angular/core';
import { UserInformationService } from 'src/app/services/user-information.service';
import { UserInformationDTO } from 'src/app/model/userInformation';
import { BasePagination } from 'src/app/core/model/base-pagination';
import { MessageService } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { UserInformationFilter } from 'src/app/model/userInformationFilter';
import { expensecategoryDto } from 'src/app/model/expensecategory';
import { ExpenseCategoryService } from 'src/app/services/expensecategory.service';
import { FormControl } from '@angular/forms';
import { UserExpenseCategoryInfo } from 'src/app/model/userExpenseCategoryInfo';
import { RoleDto } from 'src/app/model/roles';
import { RoleService } from 'src/app/services/role.service';
@Component({
  selector: 'userinfomation-coffee',
  templateUrl: './userinfomation-coffee.component.html',
  styleUrls: ['./userinfomation-coffee.component.scss']
})
export class UserinfomationCoffeeComponent  extends BasePagination<UserInformationFilter, UserInformationDTO> implements  OnInit {

    users: UserInformationDTO = new UserInformationDTO();
    isReadOnly: boolean = true; // var sayılan olarak readonly olmasını istediğimiz alanı seçtik

    expenseCategories: expensecategoryDto[] = [];// bütün harcama kategorilerini getirmek için kullanılmaktadır

    expenseCategory:expensecategoryDto=new expensecategoryDto(); //ödeme işlemlerine belirli bir harcama kategorisi eklemek veya güncellemek için kullanılmaktadır


   UserInformationDialog?: boolean;
   submitted?: boolean;
   deleteUserInformationDialog = false;
   updateUserInformationDialog?: boolean;
   isFilter: boolean = false;
   role:RoleDto=new RoleDto();
   roles: RoleDto[] = [];
   addRoleDialog?:boolean;
   updateRoleDialog?:boolean;
   loading: boolean = false;

   type: string ="password";
   isText:boolean=false;
   eyeIcon:string="pi-eye-slash";



   authoritiesDialogVisible?: boolean = false;
   selectedUserForAuthorities?: UserInformationDTO;
   selectedUserRoles: string[] = [];

   
   firstNameRequiredError: boolean = false;
   firstNameMinLengthError: boolean = false;
   firstNameMaxLengthError: boolean = false;
   
   lastNameRequiredError: boolean = false;
   lastNameMinLengthError: boolean = false;
   lastNameMaxLengthError: boolean = false;
   
   emailRequiredError: boolean = false;
   emailFormatError: boolean = false;
   
   phoneNumberRequiredError: boolean = false;
   phoneNumberMinLengthError: boolean = false;
   phoneNumberMaxLengthError: boolean = false;
   phoneNumberFormatError: boolean = false;




  constructor(public userInformationService: UserInformationService,  private messageService: MessageService,private confirmationService: ConfirmationService,
     public expensecategoryService:ExpenseCategoryService,public roleService:RoleService) {

    super(userInformationService, messageService);
    this.pFilter = new UserInformationFilter ();
   }

  ngOnInit(): void {

    this.loadExpenseCategories();
    this.loadRoleInfos();
    this.loadData(event);
    this.expenseCategories = [];
    //this.selectedSuperAdminRole = false;
  }


  validateFirstName() {
    this.firstNameRequiredError = !this.users.firstName;
    this.firstNameMinLengthError = this.users.firstName !== undefined && this.users.firstName.length < 3;
    this.firstNameMaxLengthError = this.users.firstName !== undefined && this.users.firstName.length > 50;
}

validateLastName() {
    this.lastNameRequiredError = !this.users.lastName;
    this.lastNameMinLengthError = this.users.lastName !== undefined && this.users.lastName.length < 2;
    this.lastNameMaxLengthError = this.users.lastName !== undefined && this.users.lastName.length > 50;
}

validateEmail() {
  this.emailRequiredError = !this.users.email;
  this.emailFormatError = !this.emailRequiredError && !this.users.email?.includes('@');
}

validatePhoneNumber() {
  this.phoneNumberRequiredError = !this.users.phoneNumber;
  
  // Örnek telefon numarası uzunluğu ve deseni kontrolü
  // Telefon numarasının 10 haneli olup olmadığını kontrol etme
  this.phoneNumberMinLengthError = this.users.phoneNumber !== undefined && this.users.phoneNumber.length < 10;
  this.phoneNumberMaxLengthError = this.users.phoneNumber !== undefined && this.users.phoneNumber.length > 11;

  // Belirli bir desene uygunluğunu kontrol etme (Örnek: Sadece sayılardan oluşan bir telefon numarası)
  const phoneRegex = /^\d+$/; // Sadece sayılardan oluşan telefon numarası
  this.phoneNumberFormatError = this.users.phoneNumber !== undefined && !phoneRegex.test(this.users.phoneNumber);
}



 






  loadRoleInfos(){
    
    this.roleService.getRoleInfo(this.role).subscribe((response)=>{
      
    
      this.roles=response.data;
    

    });
  }




  loadExpenseCategories(){

    this.expensecategoryService.getExpenseCategories().subscribe((result)=>{
        this.expenseCategories=result.data;

        //this.users.ExpenseCategoryIds=[];
        console.log(this.expenseCategories);

    });
    };

  override loadData(event: any): void {
    
    this.pFilter = {
          expenseCategoryType: 1, // 1, ExpenseCategoryType 1'e (örneğin, Kahve) eşit olacak şekilde filtreleme yapıyor.
          // Diğer filtre özellikleri
        };
       super.loadData(event);
    }

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
    openUpdateRoleInformation(id: string){
      this.users.id = id;
      this.updateRoleDialog = true;
    }


    hideDialog() {
        this.UserInformationDialog = false;
        this.updateUserInformationDialog=false;
        this.updateRoleDialog=false;
        this.submitted = false;
    }

    hideShowPass(){
        this.isText=!this.isText;
        this.isText ? this.eyeIcon="pi-eye": this.eyeIcon="pi-eye-slash";
         this.isText ? this.type="text":this.type="password";

    }

  saveUserInfo() {

        this.submitted = true;
        debugger;

        if (this.users.firstName && this.users.lastName ) {

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
              debugger;
            if (result.isSuccess){
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Kullanıcı  Ekleme Başarılı.', life: 3000 });

            this.loadData(event);

            }
            else
            this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
            })
        }

        this.UserInformationDialog = false;
        this.updateUserInformationDialog=false;
        this.users = {};
      }
  }


    updateUserInfo(users: UserInformationDTO) {
        this.users = {...users};
        //this.UserInformationDialog = true;
        this.updateUserInformationDialog=true;

        this.isReadOnly=true;// Düzenleme modunda
    }

    updateRoleInfo(users: UserInformationDTO) {
     this.users = {...users};
      //this.UserInformationDialog = true;
      this.updateRoleDialog=true;

      this.isReadOnly=true;// Düzenleme modunda
    }




  deleteUserInfo(users: UserInformationDTO): void {

        this.confirmationService.confirm({
            message: users.firstName + ' isimli kullanıcıyı silmek istediğinize emin misiniz??',
            header: 'Uyarı',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                // userexpense.ExpenseCategoryIds=this.expenseCategory.id;


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
