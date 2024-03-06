export class MemberDetailResponse {
   
    data!:memberdetail;
    isSuccess!:boolean;
    errors!:any[];
   
   
}
export class memberdetail {
   
    id?: number;
    code!: string;
    nameEnc?: string;
    surnameEnc?: string;
    phoneNumber?: string;
    memberType?: string;
    tcknMsk?: string;
    eMail?: string;
    birthPlace?: string;
    birthDate?: string;
    city?: string;
    district?: string;
    blackList?: boolean;
    isActive?: boolean;
    isDeleted?: boolean;
    SignatureIsValid?: boolean;
}
