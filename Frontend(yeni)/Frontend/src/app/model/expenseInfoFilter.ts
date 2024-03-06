export class ListExpenseInfoFilter{
    id?: string="";
    description?:string="";
    paymentDate?: Date;
    amount?:number=0;
    expenseCategoryId?: string="";
    expenseTypeId?: string="";
    expenseCategoryName?:string="";
    expenseCategoryType?:number=0;
    expenseTypeName?: string;
}