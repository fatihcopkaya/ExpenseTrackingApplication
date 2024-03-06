import { th } from "date-fns/locale";
import { MessageService } from "primeng/api";
import { Table } from "primeng/table";
import { PaginationFilter } from "../../model/paginationFilter";
import { PaginationService } from "../../services/pagination-service";

export class BasePagination<T,TResponse> {
    data: TResponse[] = [];
    totalRecords: number = 0;
    rowsPerPageOptions = [10, 50, 100];
    rows=10
    page=1

    private _tService: PaginationService<TResponse>;
    private __messageService: MessageService;
    cdr: any;
    constructor(tService: PaginationService<TResponse>, ___messageservice:MessageService ) {
        this._tService = tService;
        this.__messageService = ___messageservice;
    }
    pFilter: any;
    mapFilter: Map<string, string> = new Map<string, string>();

    getFilter(event: any, columnFilter?: any, selectedItem: string = "id", type?: string) {


     debugger;

        if(event && event.target && event.target.value && type && type == "number"){
            columnFilter.dt.filter(event.target.value, columnFilter.field, columnFilter.matchMode);
        }
        else if(event && type && type == "date"){
            columnFilter.dt.filter(event, columnFilter.field, columnFilter.matchMode);
        }



        else if (event && event.type == 'input' && (event.target.value.length > 2)) {
            columnFilter.dt.filter(event.target.value, columnFilter.field, columnFilter.matchMode);
        }
        else if (event && event.type == 'input' && (event.target.value.length == 0)) {
            columnFilter.clearFilter();
        }
        else if (event && event.originalEvent && event.originalEvent.type == "click") {
            if (!this.mapFilter.has(columnFilter.field) && selectedItem) {
                this.mapFilter.set(columnFilter.field, selectedItem);
            }
            if (event.value && event.value.length && event.value.length> 0) {
                columnFilter.dt.filter(event.value, columnFilter.field, columnFilter.matchMode);
            }
            else if(event.value && event.value.length == undefined){
                columnFilter.dt.filter(event.value, columnFilter.field, columnFilter.matchMode);
            }
            else {
                columnFilter.clearFilter();
            }
        }
       else if (event && event.type==undefined)
        {
            columnFilter.dt.filter(event, columnFilter.field, columnFilter.matchMode);
        }
    }





    loadData(event?: any) {
        // //debugger;
        if (event != null) {
            this.matchFilters(event);
        }





        if(event && event.rows){
            this.pFilter.pageSize = event.rows;
            this.rows = event.rows;
            this.page = event && event.first && event.rows ? (event.first / (event.rows ? event.rows : this.rows)) + 1 : 1;
        }
        else{
            this.pFilter.pageSize = this.rows;
        }
        this.pFilter.pageSize = event && event.rows ? event.rows : this.rows;
        this.pFilter.pageNumber = event && event.first && event.rows ? (event.first / (event.rows ? event.rows : this.rows)) + 1 : this.page;
        this.pFilter.sortField = event && event.sortField ?  event.sortField : "";
        this.pFilter.sortOrder = event && event.sortOrder ? event.sortOrder : -1;
        this._tService.getPagination(this.pFilter).subscribe(p => {
        // //debugger;
            if(p.isSuccess){

                 this.data = p.items;
            this.totalRecords = p.totalCount > 0 ? p.totalCount : 0;
            }
           else if(p.errors && p.errors.length > 0){
            this.__messageService.add({ severity: 'error', summary: "", detail: p.errors ?  p.errors.join(',') : 'İşlem başarısız.', life: 3000 });
        }
        this.data = p.items;
        //this.totalRecords = 2;
        });
    }

    matchFilters(event: any) {
        if (event && event.filters) {
            let tableElements = Object.entries(event.filters);
            let newElements: [string, any][] = [];
            tableElements.forEach(([keyTableFilter, valueTableFilter]) => {
                let valueParse = <any>valueTableFilter;
                let value = valueParse["value" as keyof typeof valueParse];
                let selectedItemName = this.mapFilter.get(keyTableFilter);


                if (selectedItemName && value && value.length && value.length > 0) {
                    let val = value.map(function (item: { selectedItemName: any; }) {
                        return item[selectedItemName as keyof typeof item];
                    });
                    newElements.push([keyTableFilter, val]);
                }
                else if (selectedItemName && value) {

                    newElements.push([keyTableFilter, value[selectedItemName]]);
                }
                else {
                    newElements.push([keyTableFilter, value]);
                }

            });
            let fromEntiries = (Object.fromEntries(newElements));
            this.pFilter = Object.assign(this.pFilter, (<T>fromEntiries));
        }

    }
}
