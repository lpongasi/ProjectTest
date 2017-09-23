
import * as React from 'react';
import { connect } from 'react-redux';
import * as $ from 'jquery';
import Dispatcher from '../../actions';
import { report } from '../../stateprops';
import CommonTable from '../../common/commontable';
import {
    Link
    } from 'react-router-dom';


@connect(report, Dispatcher)
export default class UserReportList extends React.Component<any, any>{

    render() {
        return (
            <div>
                <Link
                    to="/UserReport/Create"
                    className="btn"><span className="fa fa-calendar-plus-o"> </span> Create New</Link>

                <CommonTable readUrl="/UserReport/Reports" dispatcherId="REPORT_REPLIST" >
                    <table className="striped highlight responsive-table">
                        <thead>
                            <tr>
                                <th>Company Address</th>
                                <th>Company Contact</th>
                                <th>Company Name</th>
                                <th>User FullName</th>
                                <th>Email</th>
                                <th>Date</th>
                                <th>Status</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.report.repList.map((item: any, i: any) =>
                                <tr key={item.id}>
                                    <td>{item.companyAddress}</td>
                                    <td>{item.companyContact}</td>
                                    <td>{item.companyName}</td>
                                    <td>{item.fullName}</td>
                                    <td>{item.email}</td>
                                    <td>{item.date}</td>
                                    <td>{item.status}</td>
                                    <td>
                                        <Link
                                            to={'/UserReport/Detail/' + item.id}
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