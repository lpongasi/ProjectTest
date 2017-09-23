import * as React from 'react';
import {
    Link
    } from 'react-router-dom';
import { CommonTable } from './Component/Table/CommonTable';
export default class PoListTable extends React.Component<any, any> {
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
                <CommonTable readUrl="/Purchase/List" dataReturn={this.dataReturn} filter={null}>
                    <table className="striped highlight responsive-table">
                        <thead>
                            <tr>
                                <th>PO#</th>
                                <th>Company Address</th>
                                <th>Company Contact</th>
                                <th>Company Name</th>
                                <th>User FullName</th>
                                <th>Email</th>
                                <th>Date</th>
                                <th>Status</th>
                                <th>SHOW</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.data.map((item: any, i: any) =>
                                <tr key={item.id}>
                                    <td>{item.idString}</td>
                                    <td>{item.companyAddress}</td>
                                    <td>{item.companyContact}</td>
                                    <td>{item.companyName}</td>
                                    <td>{item.fullName}</td>
                                    <td>{item.email}</td>
                                    <td>{item.purchaseDateString}</td>
                                    <td>{item.poStatus}</td>
                                    <td>
                                        <Link
                                            to={'/Purchase/Detail/' + item.id} 
                                            className="btn btn-floating"><span className="fa fa-list"> </span> </Link>
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </CommonTable>
            </div >
        );
    }
}