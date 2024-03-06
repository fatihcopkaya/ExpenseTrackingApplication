import { Observable } from "rxjs";
import { BaseResultPagination } from "../core/model/base-result-pagination";
import { PaginationFilter } from "../model/paginationFilter";

export interface PaginationService<TResponse> {
    getPagination(filter: any) : Observable<BaseResultPagination<TResponse>>
}