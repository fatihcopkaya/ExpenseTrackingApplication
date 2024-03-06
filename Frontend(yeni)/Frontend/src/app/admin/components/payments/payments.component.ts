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
    import { filter } from 'jszip';
    import { BaseResultPagination } from 'src/app/core/model/base-result-pagination';
    import { ExpenseCategoryService } from 'src/app/services/expensecategory.service';
    import { expensecategoryDto } from 'src/app/model/expensecategory';
import { log } from 'console';
import { truncate } from 'fs';



    @Component({
    selector: 'app-payments',
    templateUrl: './payments.component.html',
    styleUrls: ['./payments.component.scss']
    })
    export class PaymentsComponent extends BasePagination<PaymentFilter,ListPaymentDto> implements OnInit {



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




        constructor(private paymentService:PaymentService , private userinformationservice:UserInformationService, private messageService: MessageService,
        private confirmationService: ConfirmationService , private expensecategoryService:ExpenseCategoryService) {
        super(paymentService,messageService);
        this.pFilter = new PaymentFilter ();

        }



        ngOnInit() {

            //this.loadData();
            this.expenseCategories = [];
         this.loadExpenseCategories();

         this.payments.forEach(payment => {
            if (payment.isPaid) {
              payment.isPaidDisplay = 'Paid';
            } else {
              payment.isPaidDisplay = 'Not Paid';
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



        override loadData(event?: any): void {
            this.pFilter = new PaymentFilter();
            //this.paymentService.getPagination({/*filtreleme parametrelerini burada ekleyin*/})


            super.loadData(event);
        }


        savePayment() {
            this.submitted = true;

            if (this.payment.appUserName && this.payment.id  && typeof this.payment.isPaid === 'boolean') {
            if (this.payment.id) {

                // if (this.selectedExpenseCategory) {
                //     this.payment.expenseCategoryId = this.selectedExpenseCategory.id; // Seçilen harcama kategorisini ödeme öğesine atayın
                // } else {
                //     console.error("selectedExpenseCategory null.");
                //     // veya varsayılan bir değer atayabilirsiniz:
                //     // this.payment.expenseCategoryId = varsayılanDeger;
                // }


                this.paymentService.updatePayments(this.payment).subscribe(result=>{
                    //debugger;
                if (result.isSuccess){

                this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Kullanıcı Grubu Güncelleme Talebi İletildi.', life: 3000 });
               // this.updatePaymentStatus(this.payment.isPaid);


                this.loadData(event);
                }
                else
                this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
                })

            }

            // else {

            //     this.paymentService.createUserInfo(this.users).subscribe(result=>{
            //     if (result.isSuccess){
            //     this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Kullanıcı Grubu Ekleme Talebi İletildi.', life: 3000 });

            //     this.loadData(event);

            //     }
            //     else
            //     this.messageService.add({ severity: 'error', summary: 'Error', detail: result.errors[0], life: 3000 });
            //     })
            // }

            this.PaymentDialog = false;
            this.payment = {};

        }
        }



        deletePayment(payment: ListPaymentDto){

            this.confirmationService.confirm({
                message: payment.appUserName + ' isimli kullanıcı grubunu silmek istediğinize emin misiniz??',
                header: 'Uyarı',
                icon: 'pi pi-exclamation-triangle',
                accept: () => {
                this.paymentService.deletePayment(payment.id).subscribe(result=>{
                    if (result.isSuccess){
                        console.log();

                    this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Kullanıcı Grubu Silme Talebi İletildi.', life: 3000 });

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
           //debugger;
            this.payment = { ...payment };

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

