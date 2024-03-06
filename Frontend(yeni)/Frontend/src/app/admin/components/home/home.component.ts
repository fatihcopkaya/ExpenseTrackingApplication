import { Component, OnInit} from '@angular/core';
import { MenuItem } from 'primeng/api';
import { LayoutService } from 'src/app/core/services/app.layout.service';
import { Subscription } from 'rxjs';
import { UserCountDTO } from 'src/app/model/usercount';
import { UserCountService } from 'src/app/services/user-count.service';
import { MessageService} from 'primeng/api';
import { ConfirmationService } from 'primeng/api';
import { PaymentListDto } from 'src/app/model/paymentInfo';
import { PaymentInfoFilter } from 'src/app/model/paymentInfoFilter';
import { PaymentHomeService } from 'src/app/services/payment-home.service';
import { WalletDto } from 'src/app/model/wallet';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
    basicData: any;
    items!: MenuItem[];
    chartData: any;
    AppUserCount: UserCountDTO = new UserCountDTO();
    loading: boolean = false;
    chartOptions: any;
    paymentList:PaymentListDto= new PaymentListDto();
    walletInfo:WalletDto=new WalletDto();
    subscription!: Subscription;
    data: any;
    basicOptions: any;
    totalAmountType1: number = 0;
    totalAmountType2: number = 0;
    totalPayments: number[] = [];
    totalExpenses: number[] = [];
    constructor(public userCountService: UserCountService,public paymentHomeService:PaymentHomeService, private messageService: MessageService,private confirmationService: ConfirmationService) {

    }

    ngOnInit() {

        this.countUser();
        this.fetchTotalAmountForType1(1);
        this.fetchTotalAmountForType1(2);
        this.paymentHomeService.getTotalPaymentAndExpense().subscribe(result => {
    
        this.walletInfo.totalPayments = result.totalPayments ;
        this.walletInfo.totalExpenses = result.totalExpenses ;
        this.createtable();
      });
        //this.fetchTotalAmountForType2();
    }



    createtable(){
        const documentStyle = getComputedStyle(document.documentElement);
        const textColor = documentStyle.getPropertyValue('--text-color');
        const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
        const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

        this.data = {
            labels: [''],
            datasets: [
              {
                label: 'Toplam Ödeme',
                backgroundColor: '#42A5F5',
                data: this.walletInfo.totalPayments // Şu anda tek bir değer dizisi
              },
              {
                label: 'Toplam Harcama',
                backgroundColor: '#FFA726',
                data: this.walletInfo.totalExpenses // Şu anda tek bir değer dizisi
              }
            ]
        };

        this.basicOptions = {
            plugins: {
                legend: {
                    labels: {
                        color: textColor
                    }
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: {
                        color: textColorSecondary

                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: true
                    }
                },
                x: {
                    ticks: {
                        color: textColorSecondary
                    },
                    grid: {
                        color: surfaceBorder,
                        drawBorder: true
                    }
                }
            }
        };
    }


    countUser(){
        this.userCountService.getAppUserCount().subscribe(result => {

            if (result.isSuccess) {

                this.AppUserCount.appUserCount = result.data.appUserCount;

            }
        });
    }

    fetchTotalAmountForType1(expenseCategoryType:number) {
        this.paymentHomeService.getTotalAmount(expenseCategoryType).subscribe(result => {

            if(result.isSuccess){
                if(expenseCategoryType === 1) {
                    this.totalAmountType1 = result.data.totalAmount;
                } else if(expenseCategoryType === 2) {
                    this.totalAmountType2 = result.data.totalAmount;
                }
            }
        });
    }







}
