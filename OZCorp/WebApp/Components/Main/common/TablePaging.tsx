﻿import * as React from 'react';
import ITablePagingProps from './TablePagingProps';

export default class TablePaging extends React.Component<ITablePagingProps, any> {
    constructor(props: any) {
        super(props);
    }
    render() {
        const pages = [];
        let startPage = 1;
        let endPage;
        if (this.props.page >= 7)
            startPage = this.props.page - 5;
        if (this.props.page + 4 <= this.props.pageCount && this.props.page > 5)
            endPage = this.props.page + 4;
        else if (this.props.pageCount >= 10 && startPage <= 5)
            endPage = 10;
        else
            endPage = this.props.pageCount;

        for (let i = startPage; i <= endPage; i++) {
            pages.push(i);
        }
        return (
            <div className="row hidden-print">
                <div className="col s12">
                    <ul className="pagination">
                        <li><a>Page </a></li>
                        {
                            this.props.page <= 1
                                ? <li className="content-padding-right">
                                    <i className="fa fa-dot-circle-o"> </i>
                                </li>
                                : <li><a onClick={() => this.props.onPageChange(this.props.page - 1)}><i className="fa fa-arrow-left"> </i> </a></li>
                        }
                        {pages.map((item, i) => this.props.page === item
                            ? <li className="black active" key={i}><a>{item}</a></li>
                            : <li className="waves-effect" key={i}><a onClick={() => this.props.onPageChange(item)}>{item}</a></li>)
                        }
                        {
                            this.props.page >= this.props.pageCount
                                ? <li className="content-padding-left">
                                      <i className="fa fa-dot-circle-o"> </i>
                                  </li>
                                : <li><a onClick={() => this.props.onPageChange(this.props.page + 1)}><i className="fa fa-arrow-right"></i></a></li>
                        }
                        <li><a>{this.props.total == null ? 0 : this.props.total} entries found</a></li>
                    </ul>
                </div>
            </div>
        );
    }
}