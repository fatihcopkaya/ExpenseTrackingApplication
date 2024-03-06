export class Map {
    lat?: any;
    lon?: any;
    address?: string;
    get showAddress(): string {
        return this.address && this.address.length > 55 ? this.address.substring(0,55) + '...': this.address!; 
    } 
    
}
