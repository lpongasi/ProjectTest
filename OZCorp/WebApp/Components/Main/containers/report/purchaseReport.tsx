    
import * as React from 'react';
import { connect } from 'react-redux';
import * as $ from 'jquery';
import Dispatcher from '../../actions';
import { purchaseReport } from '../../stateprops';
import CommonTable from '../../common/PurchaseReportTable';
import {
    Link
    } from 'react-router-dom';

@connect(purchaseReport, Dispatcher)
export default class PurchaseReportList extends React.Component<any, any>{
    constructor(props: any) {
        super(props);
        this.imageLarge = this.imageLarge.bind(this);
        this.imageLargeClose = this.imageLargeClose.bind(this);
    }
    imageLarge(e: React.MouseEvent<HTMLImageElement>) {
        $(this.refs['modal-image']).attr('src', e.currentTarget.src.replace('.', '-Orig.'));
        $(this.refs['modal']).css('display', 'block');
    }
    imageLargeClose() {
        $(this.refs['modal']).css('display', 'none');
    }

    render() {
        
        return (
            <div>
                <div className="modal-image" ref="modal" onClick={this.imageLargeClose}>
                    <img className="modal-content" ref="modal-image" />
                </div>
                <h4><span className="fa fa-file-excel-o"></span> Purchase Report</h4>
                <CommonTable readUrl="/Manage/Report/List" dispatcherId="PURCHASE_REPORT" >
                    <h5 className="right">TOTAL : {this.props.purchaseReport.itemReportViewTotal != null ? this.props.purchaseReport.itemReportViewTotal.totalString : '0'}</h5>
                     <table className="striped highlight">
                        <thead>
                            <tr>
                                <th></th>
                                <th>Barcode</th>
                                <th>Item name</th>
                                <th>Size</th>
                                <th>Description</th>
                                <th>Category</th>
                                <th>Sub Category</th>
                                <th>Price</th>
                                <th>Quantity</th>
                                <th>Total</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.purchaseReport.itemReportViews!=null?this.props.purchaseReport.itemReportViews.map((item: any, i: any) =>
                                <tr key={item.id}>
                                    <th>
                                        <img src={item.imageLocation} width="50" height="50" onClick={this.imageLarge} alt="" />
                                    </th>
                                    <td>{item.barcode}</td>
                                    <td>{item.name}</td>
                                    <td>{item.size}</td>
                                    <td>{item.description}</td>
                                    <td>{item.category}</td>
                                    <td>{item.subCategory}</td>
                                    <td>{item.priceString}</td>
                                    <td>{item.quantity}</td>
                                    <td>{item.totalString}</td>
                                </tr>
                            ):<tr></tr>}
                        </tbody>
                     </table>
                    <h5 className="right">TOTAL : {this.props.purchaseReport.itemReportViewTotal != null ? this.props.purchaseReport.itemReportViewTotal.totalString : '0'}</h5>
                </CommonTable>
            </div>
        );
    }
}