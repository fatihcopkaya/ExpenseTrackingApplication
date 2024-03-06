import { BaseResult } from "./base-result";

export class BaseResultPagination<T> extends BaseResult{
    items:Array<T> = [];
    totalCount: number = 0;
}