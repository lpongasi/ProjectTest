import * as React from 'react';
import { connect } from 'react-redux';
import * as $ from 'jquery';
import TableSearchArea from './TableSearchArea';
import TablePaging from './TablePaging';
import Dispatcher from '../actions';
interface ICommonTableProps {
    readUrl:string;
    dispatcherId:string;
}
class CommonTableState {
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

@connect(null,Dispatcher)
export  default class  CommonTable extends React.Component<ICommonTableProps, CommonTableState>  {
    constructor(props: any) {
        super(props);
        this.state = new CommonTableState();
        this.onPageChange = this.onPageChange.bind(this);
        this.serchFilterChange = this.serchFilterChange.bind(this);
        this.pageSizeChange = this.pageSizeChange.bind(this);
        this.filterData = this.filterData.bind(this);
        this.dataChange = this.dataChange.bind(this);
    }
    filterData() {
        this.setState({ tableStatus: 'Filtering data...', data: [] }, this.dataChange);
        $.post(this.props.readUrl, {
            filter: this.state.filter,
            page: this.state.filterPage,
            pageSize: this.state.filterPageSize
        }, data => {
                this.setState({
                    data: data.data,
                    page: data.page,
                    pageSize: data.pageSize,
                    total: data.total,
                    pageCount: data.pageCount,
                    tableStatus: data.data.length <= 0 ? 'NO Data Found' : ''
                }, this.dataChange);
            },
            'json'
        ).fail(() => {
            this.setState({
                tableStatus: 'Error Found! Please contact your system administrator!'
            }, this.dataChange);
        });
    }
    dataChange() {
            this.props.Dispatch(this.props.dispatcherId, this.state.data);
    }
    serchFilterChange(filter: string) {
        this.setState({
            filter: filter,
            filterPage: 1
        }, this.filterData);
    }
    pageSizeChange(pageSize: number) {
        this.setState({
            filterPage: 1,
            filterPageSize: pageSize
        }, this.filterData);
    }
    onPageChange(page: number) {
        this.setState({
            filterPage: page
        }, this.filterData);
    }
    componentDidMount() {
        this.filterData();
    }

    render() {
        return (
            <div className="row">
                <TableSearchArea onPageSizeChange={this.pageSizeChange} onSearch={this.serchFilterChange} />
                {this.props.children}
                <div className="row center content-padding">{this.state.tableStatus}</div>
                <TablePaging total={this.state.total} page={this.state.page} pageCount={this.state.pageCount} onPageChange={this.onPageChange} />
            </div>
        );
    }
}