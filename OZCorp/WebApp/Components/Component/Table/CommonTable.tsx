import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as $ from 'jquery';
import { CommonTableState } from './CommonTableState';
import { TableSearchArea } from './TableSearchArea';
import { TablePaging } from './TablePaging';
export class CommonTable extends React.Component<any, CommonTableState>  {
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
        if (this.props.dataReturn)
            this.props.dataReturn(this.state.data);
    }
    serchFilterChange(filter: any) {
        this.setState({
            filter: $(filter).val(),
            filterPage: 1
        }, this.filterData);
    }
    pageSizeChange(pageSize: any, filter: any) {
        this.setState({
            filter: $(filter).val(),
            filterPage: 1,
            filterPageSize: $(pageSize).val()
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
    componentWillReceiveProps(nextProps:any) {
        // You don't have to do this check first, but it can help prevent an unneeded render
        if (nextProps.filter !== this.state.additionalFilter) {
            this.setState({ additionalFilter: nextProps.filter }, this.filterData);
        }
    }
    render() {
        return (
            <div>
                <TableSearchArea onPageSizeChange={this.pageSizeChange} onSearch={this.serchFilterChange} />
                {this.props.children}
                <div className="center content-padding">{this.state.tableStatus}</div>
                <TablePaging total={this.state.total} page={this.state.page} pageCount={this.state.pageCount} onPageChange={this.onPageChange} />
            </div>
        );
    }
}