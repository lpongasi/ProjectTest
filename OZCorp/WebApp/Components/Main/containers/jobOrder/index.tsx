
import * as React from 'react';
import { connect } from 'react-redux';
import * as $ from 'jquery';
import Dispatcher from '../../actions';
import { jobOrder } from '../../stateprops';
import CommonTable from '../../common/commontable';
import {
    Link
    } from 'react-router-dom';


@connect(jobOrder, Dispatcher)
export default class JobOrders extends React.Component<any, any>{

    render() {
        return (
            <div>
                <CommonTable readUrl="/JobOrder/List" dispatcherId="JO_FILTER" >
                    <table className="striped highlight responsive-table">
                        <thead>
                            <tr>
                                <th>JO#</th>
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
                            {this.props.jobOrder.list.map((item: any, i: any) =>
                                <tr key={item.id}>
                                    <td>{item.idString}</td>
                                    <td><a href={'/Purchase/Detail/'+item.poId}> {item.poIdString}</a></td>
                                    <td>{item.companyAddress}</td>
                                    <td>{item.companyContact}</td>
                                    <td>{item.companyName}</td>
                                    <td>{item.fullName}</td>
                                    <td>{item.email}</td>
                                    <td>{item.joDateString}</td>
                                    <td>{item.joStatus}</td>
                                    <td>
                                        <Link
                                            to={'/JobOrder/Detail/' + item.id}
                                            className="btn btn-floating"><span className="fa fa-list"> </span> </Link>
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </CommonTable>
            </div>
        );
    }
}