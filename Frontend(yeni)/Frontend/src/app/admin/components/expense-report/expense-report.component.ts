import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { BasePagination } from 'src/app/core/model/base-pagination';
import { ListExpenseDto } from 'src/app/model/expenseInfo';
import { ListExpenseInfoFilter } from 'src/app/model/expenseInfoFilter';
import { ExpenseTypeDto } from 'src/app/model/expenseType';
import { expensecategoryDto } from 'src/app/model/expensecategory';
import { ExpenseInfoService } from 'src/app/services/expense-info.service';
import { ExpenseReportService } from 'src/app/services/expense-report.service';
import { ExpenseCategoryService } from 'src/app/services/expensecategory.service';
import { ReportService } from 'src/app/services/report.service';

@Component({
  selector: 'expense-report',
  templateUrl: './expense-report.component.html',
  styleUrls: ['./expense-report.component.scss']
})
export class ExpenseReportComponent   extends BasePagination<ListExpenseInfoFilter,ListExpenseDto>  implements OnInit {

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


    startDate:string='';
    endDate:string='';

    //selectedExpenseCategory: expensecategoryDto | null = null;// dropdown için

    //expenseCategories: expensecategoryDto[] = [];// bütün harcama kategorilerini getirmek için kullanılmaktadır




    submitted?: boolean;


    updateExpenseInfoDialog=false;
    // PaymentDialog?: boolean;
    // payments: PaymentListDto[] = [];

    confirmationDialogVisible: boolean = false;
    constructor(private expenseInfoService:ExpenseInfoService ,  private messageService: MessageService,private expenseReportService:ExpenseReportService,
      private confirmationService: ConfirmationService , private expensecategoryService:ExpenseCategoryService) {
        super(expenseReportService,messageService);
        this.pFilter = new ListExpenseInfoFilter ();
    }

    ngOnInit(): void {

      this.loadExpenseCategories();

      this.loadExpenseTypes();

      this.applyDateFilter();

    }

    applyDateFilter() {
        debugger;
                this.pFilter.startDate = this.startDate;
                this.pFilter.endDate = this.endDate;

                this.loadData(event);

        }

    override loadData(event: any): void {

    

      this.pFilter = {

        startdate: this.pFilter.startDate, // Değişiklik burada
        enddate: this.pFilter.endDate // Değişiklik burada

      };
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

    hideDialog() {
      this.expenseInfoDialog = false;
      this.updateexpenseInfoDialog=false;
      this.submitted = false;
    }


}
