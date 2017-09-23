interface ITablePagingProps {
    pageCount: number;
    page: number;
    total: number;
    onPageChange(page: number): void;
}

export default ITablePagingProps;