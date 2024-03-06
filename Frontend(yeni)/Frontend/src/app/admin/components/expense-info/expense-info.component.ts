import { Component, OnInit } from '@angular/core';
import { BasePagination } from 'src/app/core/model/base-pagination';
import { ListExpenseDto } from 'src/app/model/expenseInfo';
import { ListExpenseInfoFilter } from 'src/app/model/expenseInfoFilter';
import { MessageService } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { ExpenseInfoService } from 'src/app/services/expense-info.service';
import { expensecategoryDto } from 'src/app/model/expensecategory';
import { ExpenseCategoryService } from 'src/app/services/expensecategory.service';
import { expenseCategoryFilter } from 'src/app/model/expensecategoryFilter';
import { ExpenseTypeDto } from 'src/app/model/expenseType';
import { ExpenseTypeFilterDto } from 'src/app/model/expenseTypeFilter';



@Component({
  selector: 'app-expense-info',
  templateUrl: './expense-info.component.html',
  styleUrls: ['./expense-info.component.scss']
})
export class ExpenseInfoComponent extends BasePagination<ListExpenseInfoFilter,ListExpenseDto>  implements OnInit {
  showIdColumn:boolean=false;
  showNewPeriodDialog : boolean= false;

  isReadOnly: boolean = true; // var sayılan olarak readonly olmasını istediğimiz alanı seçtik
  expenseInfo: ListExpenseDto = new ListExpenseDto();
  expenseInfos: ListExpenseDto[] = [];
  expenseInfoDialog?: boolean;
  addexpenseTypeDialog?:boolean;
  expenseType:ExpenseTypeDto= new ExpenseTypeDto();
  expenseTypes: ExpenseTypeDto[] = [];



  selectedExpenseCategory: expensecategoryDto | null = null;// dropdown için
  expenseCategories: expensecategoryDto[] = [];// bütün harcama kategorilerini getirmek için kullanılmaktadır
  expenseCategory:expensecategoryDto=new expensecategoryDto();
  deleteexpenseInfoDialog = false;
  updateexpenseInfoDialog=false;
  isFilter: boolean = false;

  loading: boolean = false;

  //selectedExpenseCategory: expensecategoryDto | null = null;// dropdown için

  //expenseCategories: expensecategoryDto[] = [];// bütün harcama kategorilerini getirmek için kullanılmaktadır




  submitted?: boolean;


  updateExpenseInfoDialog=false;
  // PaymentDialog?: boolean;
  // payments: PaymentListDto[] = [];

  confirmationDialogVisible: boolean = false;
  constructor(private expenseInfoService:ExpenseInfoService ,  private messageService: MessageService,
    private confirmationService: ConfirmationService , private expensecategoryService:ExpenseCategoryService) {
      super(expenseInfoService,messageService);
      this.pFilter = new ListExpenseInfoFilter ();
  }

  ngOnInit(): void {
    //debugger;
    //this.loadData(event);
    this.loadExpenseCategories();

    this.loadExpenseTypes();
  //   this.expensecategoryService.getExpenseCategories().subscribe((data:any)=>{
  //     this.expenseCategories=data;
  // })
  }

  override loadData(event: any): void {

    this.pFilter = new ListExpenseInfoFilter();
    super.loadData(event);
 }
 loadExpenseCategories(){

   this.expensecategoryService.getExpenseCategories().subscribe((result)=>{

      this.expenseCategories=result.data;
      //console.log(this.expenseCategories);

    });
  }
  loadExpenseTypes(){
    this.expenseInfoService.getExpenseTypes(this.expenseType).subscribe((result)=>{
     
       this.expenseTypes=result.expenseTypes;
       //console.log(this.expenseTypes);

     });
   }
  openNew() {
    this.expenseInfo = {};
    this.submitted = false;
    this.expenseInfoDialog = true;
    this .isReadOnly=false; // Ekleme yapılabilir modda
  }
  openNewAddExpenseType() {
    this.expenseInfo = {};
    this.submitted = false;
    this.addexpenseTypeDialog = true;
    this .isReadOnly=false; // Ekleme yapılabilir modda
  }
  openUpdateUserInformation(id: string){
    this.expenseInfo.id = id;
    this.updateexpenseInfoDialog = true;
  }
  hideDialog() {
    this.expenseInfoDialog = false;
    this.updateexpenseInfoDialog=false;
    this.submitted = false;
  }
    saveExpenseInfo() {
      //debugger;
      this.submitted = true;

      if (this.expenseInfo.expenseTypeId && this.expenseInfo.amount ) {

        if (this.expenseInfo.id) {
          this.expenseInfoService.updateExpenseInfo(this.expenseInfo).subscribe(result=>{
          if (result.isSuccess){

          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Harcama Güncelleme Talebi İletildi.', life: 3000 });
          this.loadData(event);
          }
          else

          this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
          })
        }
      else {

          this.expenseInfoService.createExpenseInfo(this.expenseInfo).subscribe(result=>{
            //debugger;
          if (result.isSuccess)
          this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Harcama Ekleme Talebi İletildi.', life: 3000 });
          else
          this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
          })
      }

      this.expenseInfoDialog = false;
      this.expenseInfo = {};
    }
  }
  editExpenseInfo(expenseInfo: ListExpenseDto) {
    if (expenseInfo.paymentDate) {
      this.expenseInfo = {...expenseInfo, paymentDate: new Date(expenseInfo.paymentDate)};
    } else {
      this.expenseInfo = {...expenseInfo};
    }
    this.expenseInfoDialog = true;
    this.updateExpenseInfoDialog=true;
  }

  deleteExpenseInfo(expenseInfo: ListExpenseDto) {
    //debugger;
    this.confirmationService.confirm({
        message: expenseInfo.description + ' isimli harcamayı silmek istediğinize emin misiniz?',
        header: 'Uyarı',
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
        this.expenseInfoService.deleteExpenseInfo(expenseInfo.id).subscribe(result=>{
            if (result.isSuccess){
              this.loadData(event);
            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Harcama Silme Başarılı.', life: 3000 });
            }
            else
            this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
        })
            this.expenseInfo = {};
        }
    });
  }
  saveExpenseType() {
    this.submitted = true;
    //debugger;
  
    // ExpenseTypeDto örneği oluşturuluyor ve name özelliğine değer atanıyor
    this.expenseType = new ExpenseTypeDto();
    this.expenseType.name = this.expenseInfo.expenseTypeName; // Örnek olarak bir isim atanıyor, gerçek senaryoda kullanıcı girişi veya başka bir kaynaktan alınabilir.

    if (this.expenseType && this.expenseType.name) {
      this.expenseInfoService.createExpenseType(this.expenseType).subscribe(result => {
        if (result.isSuccess) {
          this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: 'Harcama Türü başarıyla eklendi.', life: 3000 });
          this.loadExpenseTypes(); // Yeni bir tür ekledikten sonra harcama türlerini tekrar yükle.
        } else {
          this.messageService.add({ severity: 'error', summary: 'Hata', detail: result.errors[0], life: 3000 });
        }
      });

      this.addexpenseTypeDialog = false;
      this.expenseType = new ExpenseTypeDto();
    }
  }

}
