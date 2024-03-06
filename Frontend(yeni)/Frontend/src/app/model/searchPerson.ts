
export class SearchPerson {
    constructor(id?:number, fullName?:string, firstName?:string, lastName?:string, identityNo?:string, phoneNumber?:string, email?:string, address?:string){
        this.id = id;
        this.fullName = fullName;
        this.firstName = firstName;
        this.lastName = lastName;
        this.identityNo = identityNo;
        this.phoneNumber = phoneNumber;
        this.email = email;
        this.address = address;
    }
    id?: number;
    fullName?: string;
    firstName?: string;
    lastName?:string;
    identityNo?: string;
    phoneNumber?: string;
    email?: string;
    address?: string;
}
