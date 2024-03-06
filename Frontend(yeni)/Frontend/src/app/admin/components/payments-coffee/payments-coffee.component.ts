import { Component, OnInit, SimpleChanges } from '@angular/core';
import { ProductService } from 'src/app/services/product.service';
import { Product } from 'src/app/model/productDetail';
import { UserInformationService } from 'src/app/services/user-information.service';
import { PaymentService } from 'src/app/services/payment.service';
import { BasePagination } from 'src/app/core/model/base-pagination';
import { PaymentListDto } from 'src/app/model/paymentInfo';
import { PaymentInfoFilter } from 'src/app/model/paymentInfoFilter';
import { MessageService } from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { ExpenseCategoryService } from 'src/app/services/expensecategory.service';
import { expensecategoryDto } from 'src/app/model/expensecategory';
import { UserInformationDTO } from 'src/app/model/userInformation';
import { UserInformationFilter } from 'src/app/model/userInformationFilter';
import { DatePipe } from '@angular/common';




@Component({
selector: 'app-payments-coffee',
templateUrl: './payments-coffee.component.html',
styleUrls: ['./payments-coffee.component.scss']
})
export class PaymentsCoffeeComponent extends BasePagination<PaymentInfoFilter,PaymentListDto> implements OnInit {



    showIdColumn:boolean=false;
    showNewPeriodDialog : boolean= false;


    isReadOnly: boolean = true; // var sayılan olarak readonly olmasını istediğimiz alanı seçtik
    payment: PaymentListDto = new PaymentListDto();
    user:UserInformationDTO=new UserInformationDTO();
    appuser: UserInformationDTO[] = [];
    selectedUserId: string = '';

    selectedExpenseCategory: expensecategoryDto | null = null;// dropdown için

    expenseCategories: expensecategoryDto[] = [];// bütün harcama kategorilerini getirmek için kullanılmaktadır

    expenseCategory:expensecategoryDto=new expensecategoryDto(); //ödeme işlemlerine belirli bir harcama kategorisi eklemek veya güncellemek için kullanılmaktadır
    isFilter: boolean = false;
    isPaidFilter: boolean | undefined;
    submitted?: boolean;

    
    updatePaymentDialog=false;
    PaymentDialog?: boolean;
    payments: PaymentListDto[] = [];

    confirmationDialogVisible: boolean = false;




    constructor(private paymentService:PaymentService , private userinformationservice:UserInformationService, private messageService: MessageService,
    private confirmationService: ConfirmationService , private expensecategoryService:ExpenseCategoryService) {
    super(paymentService,messageService);
    this.pFilter = new PaymentInfoFilter ();

    }



    ngOnInit() {

        // this.loadData();
        // this.expenseCategories = [];
     this.loadExpenseCategories();
     //this.loadUserName(this.user.id)
    this.loadUserName();

     this.payments.forEach(payment => {
        if (payment.isPaid) {
          payment.isPaidDisplay = 'Ödendi';
        } else {
          payment.isPaidDisplay = 'Ödenmedi';
        }
      });


}
openNew() {
    this.payment = {};
    this.submitted = false;
    this.PaymentDialog = true;
    this .isReadOnly=false;
}
openNewPeriod() {
    this.showNewPeriodDialog = true;
    this.payment = {};
    this.submitted = false;
    this .isReadOnly=false;
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




    override loadData(event?: any): void {
        this.pFilter = {
          expenseCategoryType: 1, // 1, ExpenseCategoryType 1'e (örneğin, Kahve) eşit olacak şekilde filtreleme yapıyor.
          // Diğer filtre özellikleri
          isPaid: this.isPaidFilter
        };

        super.loadData(event);
    }


    savePayment() {
        this.submitted = true;

        if (this.payment.appUserId && typeof this.payment.isPaid === 'boolean') {
        if (this.payment.id) {

            // if (this.selectedExpenseCategory) {
            //     this.payment.expenseCategoryId = this.selectedExpenseCategory.id; // Seçilen harcama kategorisini ödeme öğesine atayın
            // } else {
            //     console.error("selectedExpenseCategory null.");
            //     // veya varsayılan bir değer atayabilirsiniz:
            //     // this.payment.expenseCategoryId = varsayılanDeger;
            // }


            this.paymentService.updatePayments(this.payment).subscribe(result=>{

            if (result.isSuccess){

            this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Ödeme Güncelleme Talebi İletildi.', life: 3000 });
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



    deletePayment(payment: PaymentListDto){

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

    createNewPaymentPeriod(payment: PaymentListDto){

        this.confirmationService.confirm({
            message:  ' Yeni Dönem Oluşturmak istediğinizden emin misiniz?',
            header: 'Uyarı',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
            this.paymentService.createNewPeriod(payment).subscribe(result=>{
                if (result.isSuccess){
                    debugger;
                    console.log();

                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Ödeme Dönemi OluşturmaTalebi İletildi.', life: 3000 });

                this.loadData(event);
                }
                else
                this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
            })
                this.payment = {};
            }
        });
    }






    updatePayments(payment: PaymentListDto ) { // ,expenseCategory:expensecategoryDto

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
    // isPaidOptions: { label: string, value: boolean }[] = [
    //     { label: 'Paid', value: true },
    //     { label: 'Not Paid', value: false }
    // ];


    isPaidOptions: { label: string, value: boolean | undefined | null }[] = [
        {  label: '', value: null },
        { label: 'Ödendi', value: true },
        { label: 'Ödenmedi', value: false }

    ];
    onIsPaidFilterChange(event: any) {
        this.isPaidFilter = event.value;
        this.loadData(); // Verileri filtrelemek için loadData() metodunu çağırın
    }

    showConfirmationDialog(payment: PaymentListDto) {
        this.payment = payment; // Düzenlemek istediğiniz ödemeyi saklayın
        this.confirmationDialogVisible = true; // Onay kutusunu görüntüle
    }


    hideDialog() {
        this.PaymentDialog = false;
        this.updatePaymentDialog=false;
        this.submitted = false;
    }


}

