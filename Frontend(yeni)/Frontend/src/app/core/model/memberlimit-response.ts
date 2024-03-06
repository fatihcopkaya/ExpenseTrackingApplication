export class MemberLimitResponse {
   
    data!:Limits;
    isSuccess!:boolean;
    errors!:any[];
   
   
}
export class Limits {
    id!:number;
    limittype!:number;
    transactionLimit!:number;
    dailyLimit!:number;
    weeklyLimit!:number;
    monthlyLimit!:number;
    remainingLimit!:number;
}
