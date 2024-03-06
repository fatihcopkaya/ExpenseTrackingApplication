export class PaymentFilter {
    id?: string="";
    appUserId ?: string="";
    expenseCategoryId?: string="";
    paymentDate?: Date= new Date();
    paymentAmount?: number=0;
    isPaid?: boolean=false;
    appUserName?: string="";
    expenseCategoryName?:string="";
}
