import * as React from 'react';

export interface ISearchAreaProps {
    onSearch(input: any): void;
    onPageSizeChange(input: any): void;
}
export default class TableSearchArea extends React.Component<ISearchAreaProps, any> {
    pageSizeInput: any;
    searchInput: string;
    constructor(props: any) {
        super(props);
        this.inputKeyPress = this.inputKeyPress.bind(this);
        this.inputChange = this.inputChange.bind(this);
    }
    inputChange(e: React.ChangeEvent<HTMLInputElement>) {
        this.searchInput = e.target.value;
    }
    inputKeyPress(e: any, input: string) {
        if (e.key === 'Enter') {
            this.props.onSearch(input);
        }
    }
    
    render() {
        return (
            <div className="row">
                <div className="col s1 hide-on-med-and-down hidden-print">
                    <label><span className="fa fa-list"> </span> Show</label>
                    <select className="browser-default max-width-70" onChange={(e) => this.props.onPageSizeChange(+e.target.value)}>
                        <option value={10}>10</option>
                        <option value={15}>15</option>
                        <option value={20}>20</option>
                        <option value={50}>50</option>
                        <option value={100}>100</option>
                        <option value={150}>150</option>
                    </select>
                </div>
                <div className="common-field col s9">
                    <label htmlFor="searchTxt"><span className="fa fa-search"> </span> Search</label>
                    <input type="text" onChange={this.inputChange} onKeyPress={(e) => this.inputKeyPress(e, e.currentTarget.value)} />
                </div>
                <div className="col s1 hidden-print">
                    <a className="btn waves-effect waves-light btn-grid-search hide-on-med-and-down" onClick={() => this.props.onSearch(this.searchInput)} >Search</a>
                    <a className="btn-grid-search hide-on-large-only" onClick={() => this.props.onSearch(this.searchInput)} >
                        <span className="fa fa-search fa-3x content-padding-top black-text"></span>
                    </a>
                </div>
            </div>
        );
    }
}