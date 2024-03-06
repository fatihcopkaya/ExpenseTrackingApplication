export class expenseCategoryFilter {
    id?: string="";
    name?:string = "";
    userExpenseCategory?:string="";
    createdDate?:Date= new Date();
    updatedDate?:Date= new Date();
    isDeleted?: boolean=false;
    expenseCategoryType?:number=0;
}
