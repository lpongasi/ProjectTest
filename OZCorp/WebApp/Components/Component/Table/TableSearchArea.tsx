import * as React from 'react';
import { ITableSearchAreaProps } from './TableSearchAreaProps';
export class TableSearchArea extends React.Component<ITableSearchAreaProps, any> {
    pageSizeInput: any;
    searchInput: any;
    constructor(props: any) {
        super(props);
        this.inputKeyPress = this.inputKeyPress.bind(this);
    }
    inputKeyPress(e: any) {
        if (e.key === 'Enter') {
            this.props.onSearch(this.searchInput);
        }
    }
    render() {
        return (
            <div className="row">
                <div className="col s1 hide-on-med-and-down">
                    <label><span className="fa fa-list"> </span> Show</label>
                    <select className="browser-default max-width-70" ref={(input) => { this.pageSizeInput = input }} onChange={() => this.props.onPageSizeChange(this.pageSizeInput, this.searchInput)}>
                        <option value="10">10</option>
                        <option value="15">15</option>
                        <option value="20">20</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                </div>
                <div className="col s9">
                    <div className="common-field">
                        <label htmlFor="searchTxt"><span className="fa fa-search"> </span> Search</label>
                        <input type="text" ref={(input) => { this.searchInput = input }} onKeyPress={(e) => this.inputKeyPress(e)} />
                    </div>
                </div>
                <div className="col s1">
                    <a className="btn waves-effect waves-light btn-grid-search hide-on-med-and-down" onClick={() => this.props.onSearch(this.searchInput)} >Search</a>
                    <a className="btn-grid-search hide-on-large-only" onClick={() => this.props.onSearch(this.searchInput)} >
                        <span className="fa fa-search fa-3x content-padding-top black-text"></span>
                    </a>
                </div>
            </div>
        );
    }
}