import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as $ from 'jquery';
import { CommonTable } from './Component/Table/CommonTable';
const elementId = 'historyTable';
export default class HistoryTable extends React.Component<any, any> {

    _url_Read: string = $('#' + elementId).attr('url-read');

    constructor(props: any) {
        super(props);
        this.state = {
            data: []
        };
        this.dataReturn = this.dataReturn.bind(this);
    }
    dataReturn(data: any[]) {
        this.setState({ data: data });
    }
    render() {
        return (
            <div className="content-padding-top">
                <h4>History</h4>
                <CommonTable readUrl={this._url_Read} dataReturn={this.dataReturn}>
                    <table className="striped highlight responsive-table">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Action</th>
                                <th>Date</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.data.map((item: any, i: any) =>
                                <tr key={item.id}>
                                    <td>{item.fullName}</td>
                                    <td>
                                        <ul>
                                            {item.action.split(', ').map((item, key) => 
                                                <li key={key}>{item}</li>
                                            )}
                                        </ul>
                                    </td>

                                    <td>{item.actionDateString}</td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </CommonTable>
            </div >
        );
    }
}
const element = document.getElementById(elementId);
if (element) {
    ReactDOM.render(
        <HistoryTable/>,
        document.getElementById(elementId)
    );
}
