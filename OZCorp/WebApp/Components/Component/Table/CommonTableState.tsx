export class CommonTableState {
    filter: any = null;
    additionalFilter: any = null;
    filterPage: number = 1;
    filterPageSize: number = 10;
    data: any[] = [];
    page: number = 1;
    pageSize: number = 10;
    total: number = 0;
    pageCount: number = 0;
    tableStatus: any;
}