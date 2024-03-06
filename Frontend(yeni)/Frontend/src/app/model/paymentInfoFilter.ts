export class PaymentInfoFilter {
    id?: string="";
    appUserId ?: string="";
    expenseCategoryId?: string="";
    paymentDate?: Date=new Date();
    periodDate?:Date=new Date();
    startdate?:string="";
    enddate?:string="";
    paymentAmount?: number=0;
    isPaid?: boolean=false;
    appUserName?:string="";
    expenseCategoryName?:string="";
    expenseCategoryType?:number;
}
