
import * as React from 'react';
import { connect } from 'react-redux';
import * as $ from 'jquery';
import Dispatcher from '../../actions';
import { companyPurchaseReport } from '../../stateprops';
import CommonTable from '../../common/CompanyReportTable';
import {
    Link
    } from 'react-router-dom';

@connect(companyPurchaseReport, Dispatcher)
export default class CompanyReportList extends React.Component<any, any>{
    render() {
        return (
            <div>
                <h4><span className="fa fa-file-excel-o"></span> Company Purchase Reports</h4>
                <CommonTable readUrl="/Manage/Report/CompanyList" dispatcherId="COMPANYPURCHASE_REPORT" >
                    <h5 className="right">TOTAL : {this.props.companyPurchaseReport.companyReportViewTotal!=null?this.props.companyPurchaseReport.companyReportViewTotal.totalString:'0'}</h5>
                     <table className="striped highlight">
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Contact</th>
                                <th>Address</th>
                                <th>Gross Income</th>
                                <th>Current Discount</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.companyPurchaseReport.companyReportViews != null ? this.props.companyPurchaseReport.companyReportViews.map((item: any, i: any) =>
                                <tr key={item.companyName}>
                                    <td>{item.companyName}</td>
                                    <td>{item.companyContact}</td>
                                    <td>{item.companyAddress}</td>
                                    <td><span className="right">{item.stringTotal}</span></td>
                                    <td><span className="right">{item.stringDiscount}</span></td>
                                </tr>
                            ):<tr></tr>}
                        </tbody>
                     </table>
                    <h5 className="right">TOTAL : {this.props.companyPurchaseReport.companyReportViewTotal != null ? this.props.companyPurchaseReport.companyReportViewTotal.totalString : '0'}</h5>
                </CommonTable>
            </div>
        );
    }
}