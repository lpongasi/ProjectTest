import * as React from 'react';
import { connect } from 'react-redux';
import * as $ from 'jquery';
import Category from './Categories';
import TableSearchArea from './TableSearchArea';
import TablePaging from './TablePaging';
import Dispatcher from '../actions';
interface ICommonTableProps {
    readUrl: string;
    dispatcherId: string;
}
class CommonTableState {
    filter: any = null;
    dateFrom: any;
    dateTo: any;
    additionalFilter: any = null;
    filterPage: number = 1;
    filterPageSize: number = 10;
    filterCategory: any = null;
    filterSubCategory: any = null;
    data: any = null;
    page: number = 1;
    pageSize: number = 10;
    total: number = 0;
    pageCount: number = 0;
    tableStatus: any;
}


@connect(null, Dispatcher)
export default class PurchaseReportTable extends React.Component<ICommonTableProps, CommonTableState>  {
    constructor(props: any) {
        super(props);
        this.state = new CommonTableState();
        this.onPageChange = this.onPageChange.bind(this);
        this.serchFilterChange = this.serchFilterChange.bind(this);
        this.pageSizeChange = this.pageSizeChange.bind(this);
        this.filterData = this.filterData.bind(this);
        this.dataChange = this.dataChange.bind(this);
        this.onCategoryChange = this.onCategoryChange.bind(this);
    }

    onCategoryChange(categoryId: any, subCategoryId: any) {
        this.setState({
            filterCategory: categoryId,
            filterSubCategory: subCategoryId
        }, this.filterData);
    }
    filterData() {
        this.setState({ tableStatus: 'Filtering data...', data: null }, this.dataChange);
        $.post(this.props.readUrl, {
            filter: this.state.filter,
            dateFrom: $(this.refs['dateFrom']).val(),
            dateTo: $(this.refs['dateTo']).val(),
            page: this.state.filterPage,
            pageSize: this.state.filterPageSize,
            categoryId: this.state.filterCategory,
            subCategoryId: this.state.filterSubCategory
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
        $('.datepicker').pickadate({
            selectMonths: true,
            selectYears: 50
        });
    }

    render() {
        return (
            <div className="row">
                <div className="row">
                    <div className="common-field col s6">
                        <label htmlFor="searchTxt"><span className="fa fa-calendar"> </span> Date from</label>
                        <input type="date" className="datepicker" ref="dateFrom" />
                    </div>
                    <div className="common-field col s6">
                        <label htmlFor="searchTxt"><span className="fa fa-calendar"> </span> Date To</label>
                        <input type="date" className="datepicker" ref="dateTo" />
                    </div>
                </div>
                <Category onCategoryChange={this.onCategoryChange} />
                <TableSearchArea onPageSizeChange={this.pageSizeChange} onSearch={this.serchFilterChange} />
                {this.props.children}
                <div className="row center content-padding">{this.state.tableStatus}</div>
                <TablePaging total={this.state.total} page={this.state.page} pageCount={this.state.pageCount} onPageChange={this.onPageChange} />
            </div>
        );
    }
}