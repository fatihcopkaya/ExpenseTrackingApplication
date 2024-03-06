import { Component, OnInit, SimpleChanges } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { Product } from 'src/app/model/productDetail';
import { UserInformationService } from 'src/app/services/user-information.service';
import { PaymentService } from 'src/app/services/payment.service';
import { BasePagination } from 'src/app/core/model/base-pagination';
import { ListPaymentDto } from 'src/app/model/payment';
import { PaymentFilter } from 'src/app/model/paymentFilter';
import { MessageService } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { ExpenseCategoryService } from 'src/app/services/expensecategory.service';
import { expensecategoryDto } from 'src/app/model/expensecategory';
import { UserInformationDTO } from 'src/app/model/userInformation';

@Component({
  selector: 'app-payment-football',
  templateUrl: './payment-football.component.html',
  styleUrls: ['./payment-football.component.scss']
})
export class PaymentFootballComponent extends BasePagination<PaymentFilter,ListPaymentDto> implements OnInit {

  showIdColumn:boolean=false;
  showNewPeriodDialog : boolean= false;
  isReadOnly: boolean = true; // var sayılan olarak readonly olmasını istediğimiz alanı seçtik
  payment: ListPaymentDto = new ListPaymentDto();
  selectedExpenseCategory: expensecategoryDto | null = null;// dropdown için
  expenseCategories: expensecategoryDto[] = [];// bütün harcama kategorilerini getirmek için kullanılmaktadır
  expenseCategory:expensecategoryDto=new expensecategoryDto(); //ödeme işlemlerine belirli bir harcama kategorisi eklemek veya güncellemek için kullanılmaktadır
  submitted?: boolean;
  updatePaymentDialog=false;
  PaymentDialog?: boolean;
  payments: ListPaymentDto[] = [];
  confirmationDialogVisible: boolean = false;
  user:UserInformationDTO=new UserInformationDTO();
  appuser: UserInformationDTO[] = [];
  isPaidFilter: boolean | undefined;

  constructor(private paymentService:PaymentService , private userinformationservice:UserInformationService, private messageService: MessageService,
    private confirmationService: ConfirmationService , private expensecategoryService:ExpenseCategoryService) {
    super(paymentService,messageService);
    this.pFilter = new PaymentFilter ();

  }


  ngOnInit(): void {
    this.expenseCategories = [];
         this.loadExpenseCategories();
         this.loadUserName();

         this.payments.forEach(payment => {
            if (payment.isPaid) {
              payment.isPaidDisplay = 'Ödendi';
            } else {
              payment.isPaidDisplay = 'Ödenmedi';
            }
          });
  }

  loadExpenseCategories(){

    this.expensecategoryService.getExpenseCategories().subscribe((result)=>{
        this.expenseCategories=result.data;
        console.log(this.expenseCategories);

         // Harcama kategorileri yüklendiğinde, ilk kategoriyi seçebilirsiniz.
    // if (this.expenseCategories.length > 0) {
    //     this.selectedExpenseCategory = this.expenseCategories[0]; // Varsayılan olarak ilk kategoriyi seçer
    // }
    });
  };
  loadUserName(){


    this.userinformationservice. getUserNameForPayment(this.user).subscribe((result)=>{
        this.appuser=result.data;
        console.log(this.appuser);
    })
  }

openNew() {
  this.payment = {};
  this.submitted = false;
  this.PaymentDialog = true;
  this .isReadOnly=false;
}




    override loadData(event?: any): void {
      this.pFilter = {
        expenseCategoryType: 2, // 1, ExpenseCategoryType 1'e (örneğin, Kahve) eşit olacak şekilde filtreleme yapıyor.
        // Diğer filtre özellikleri
        isPaid: this.isPaidFilter
      };

      super.loadData(event);
  }
    savePayment() {
      this.submitted = true;

      if (this.payment.appUserId && typeof this.payment.isPaid === 'boolean') {
      if (this.payment.id) {




          this.paymentService.updatePayments(this.payment).subscribe(result=>{

          if (result.isSuccess){

          this.messageService.add({ severity: 'success', summary: 'Successful', detail: ' Ödeme Güncelleme Talebi İletildi.', life: 3000 });
         // this.updatePaymentStatus(this.payment.isPaid);


          this.loadData(event);
          }
          else
          this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
          })

      }

      else {


        this.paymentService.createPayments(this.payment).subscribe(result=>{
        if (result.isSuccess){
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Ödeme Ekleme Talebi İletildi.', life: 3000 });

        this.loadData(event);

        }
        else
        this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
        })
    }

    this.PaymentDialog = false;
    this.payment = {};



  }
  }



  deletePayment(payment: ListPaymentDto){

      this.confirmationService.confirm({
          message: payment.appUserName + ' isimli Ödemeyi silmek istediğinize emin misiniz??',
          header: 'Uyarı',
          icon: 'pi pi-exclamation-triangle',
          accept: () => {
          this.paymentService.deletePayment(payment.id).subscribe(result=>{
              if (result.isSuccess){
                  console.log();

              this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Ödeme Silme Talebi İletildi.', life: 3000 });

              this.loadData(event);
              }
              else
              this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
          })
              this.payment = {};
          }
      });
  }






  updatePayments(payment: ListPaymentDto ) { // ,expenseCategory:expensecategoryDto
    
      if (payment.paymentDate) {
        this.payment = {...payment, paymentDate: new Date(payment.paymentDate)};
    } else {
        this.payment = {...payment};
    }

      this.PaymentDialog = true;
      this.updatePaymentDialog = true;

      this.isReadOnly = true; // Düzenleme modunda

  //    this.loadExpenseCategories();

    this.payment.isPaid = true; // Örnek olarak true olarak güncelleme yapalım, ihtiyaca göre bu değeri değiştirin


       this.confirmationDialogVisible = false;

  }

  updatePaymentStatus(isPaid: boolean) {
      if (isPaid !== undefined) {
          // İşlemi gerçekleştirmek için ödeme nesnesini güncelle
          this.payment.isPaid = isPaid;

          // Burada ödeme güncelleme isteğinizi sunucuya göndermek veya işlemi yerel olarak kaydetmek gibi işlemleri yapabilirsiniz.

          // İşlem tamamlandığında onay kutusunu kapatın
          this.confirmationDialogVisible = false;
        }
  }
  

  
  isPaidOptions: { label: string, value: boolean | undefined | null }[] = [
    {  label: '', value: null }, 
    { label: 'Ödendi', value: true },
    { label: 'Ödenmedi', value: false }
   
];
onIsPaidFilterChange(event: any) {
    this.isPaidFilter = event.value;
    this.loadData(); // Verileri filtrelemek için loadData() metodunu çağırın
}

  showConfirmationDialog(payment: ListPaymentDto) {
      this.payment = payment; // Düzenlemek istediğiniz ödemeyi saklayın
      this.confirmationDialogVisible = true; // Onay kutusunu görüntüle
  }


  hideDialog() {
      this.PaymentDialog = false;
      this.updatePaymentDialog=false;
      this.submitted = false;
  }


}



