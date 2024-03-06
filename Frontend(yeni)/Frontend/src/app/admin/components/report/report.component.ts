import { Component, OnInit } from '@angular/core';
import { ConfirmationService } from 'primeng/api';
import { MessageService } from 'primeng/api';

import { BasePagination } from 'src/app/core/model/base-pagination';
import { walletByExpenseCategoryDto } from 'src/app/model/walletByExpenseCategory';
import { walletByExpenseCategoryFiter } from 'src/app/model/walletByExpenseCategoryfilter';
import { ExpenseCategoryService } from 'src/app/services/expensecategory.service';
import { WalletbyexpesnecategoryService } from 'src/app/services/walletbyexpesnecategory.service';
import { expensecategoryDto } from 'src/app/model/expensecategory';
import { PaymentListDto } from 'src/app/model/paymentInfo';
import { UserInformationDTO } from 'src/app/model/userInformation';
import { PaymentService } from 'src/app/services/payment.service';
import { UserInformationService } from 'src/app/services/user-information.service';
import { PaymentInfoFilter } from 'src/app/model/paymentInfoFilter';
import { ReportService } from 'src/app/services/report.service';
import { ListExpenseDto } from 'src/app/model/expenseInfo';
import { ListExpenseInfoFilter } from 'src/app/model/expenseInfoFilter';
import { ExpenseTypeDto } from 'src/app/model/expenseType';
import { ExpenseInfoService } from 'src/app/services/expense-info.service';


@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss']
})
export class ReportComponent  extends BasePagination<PaymentInfoFilter,PaymentListDto> implements OnInit {


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


    startDate:string='';
    endDate:string='';

    constructor(private paymentService:PaymentService , private userinformationservice:UserInformationService, private messageService: MessageService,
    private confirmationService: ConfirmationService , private expensecategoryService:ExpenseCategoryService,reportService:ReportService, private expenseInfoService:ExpenseInfoService) {
    super(reportService,messageService);
    this.pFilter = new PaymentInfoFilter ();


    }



    ngOnInit() {

     this.loadExpenseCategories();
     this.loadUserName();


     this.payments.forEach(payment => {
        if (payment.isPaid) {
          payment.isPaidDisplay = 'Ödendi';
        } else {
          payment.isPaidDisplay = 'Ödenmedi';
        }
      });

      this.applyDateFilter();


}


openNew() {
    this.payment = {};
    this.submitted = false;
    this.PaymentDialog = true;
    this .isReadOnly=false;
}



applyDateFilter() {
debugger;
        this.pFilter.startDate = this.startDate;
        this.pFilter.endDate = this.endDate;

        this.loadData();

}


  formatDate(date: any): string {
    if (date) {
      const newDate = new Date(date);
      return newDate.toLocaleDateString(); // Veya uygun bir tarih formatı kullanabilirsiniz
    }
    return '';
  }


loadExpenseCategories(){

    this.expensecategoryService.getExpenseCategories().subscribe((result)=>{
        this.expenseCategories=result.data;
        console.log(this.expenseCategories);
    });
    };

 loadUserName(){


    this.userinformationservice. getUserNameForPayment(this.user).subscribe((result)=>{
        this.appuser=result.data;
        console.log(this.appuser);
    })



}



onDateRangeSelect(event: any) {
    // this.PaymentDialog=event.value;
    this.loadData(); // Veya özel bir filtreleme fonksiyonu çağırın
 }



    override loadData(event?: any): void {
        debugger;

        console.log('Start Date:', this.pFilter.startdate);
        console.log('End Date:', this.pFilter.enddate);
        this.pFilter = {
          expenseCategoryType: 1, // 1, ExpenseCategoryType 1'e (örneğin, Kahve) eşit olacak şekilde filtreleme yapıyor.
          // Diğer filtre özellikleri
          isPaid: this.isPaidFilter,
          startdate: this.pFilter.startDate, // Değişiklik burada
          enddate: this.pFilter.endDate // Değişiklik burada

        };



        super.loadData(event);
    }


    updatePaymentStatus(isPaid: boolean) {
        if (isPaid !== undefined) {
            // İşlemi gerçekleştirmek için ödeme nesnesini güncelle
            this.payment.isPaid = isPaid;

            // Burada ödeme güncelleme isteğinizi sunucuya göndermek veya işlemi yerel olarak kaydetmek gibi işlemleri yapabilirsiniz.

            // İşlem tamamlandığında onay kutusunu kapatıyor
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
        this.loadData(); // Verileri filtrelemek için loadData() metodunu çağırıyor
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
