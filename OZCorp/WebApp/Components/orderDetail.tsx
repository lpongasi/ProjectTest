import * as React from 'react';
import * as $ from 'jquery';
import UserInfo from './Common/userInfo';
import {
    Link
    } from 'react-router-dom';
export default class OrderDetail extends React.Component<any, any> {
    _url_Detail: string = '/Purchase/GetDetail';
    orderId: string = this.props.poId;
    constructor(props: any) {
        super(props);
        this.state = {
            data: null,
            isUpdate: false,
            selectedItem: null,
            itemNewQuantity: 0,
            itemNewDiscount: 0,
            itemRemarks: null,

            newTax: 0,
            otherRemarks: null,
            newOtherFees: 0,
            newDiscount: 0,
            globalRemarks: null
        };
        this.valueChange = this.valueChange.bind(this);
        this.valueTextAreaChange = this.valueTextAreaChange.bind(this);
        this.updateConfirm = this.updateConfirm.bind(this);
        this.itemUpdate = this.itemUpdate.bind(this);
        this.itemUpdateConfirm = this.itemUpdateConfirm.bind(this);
        this.itemUpdateCancel = this.itemUpdateCancel.bind(this);
        this.actionConfirm = this.actionConfirm.bind(this);
        this.printDetail = this.printDetail.bind(this);
    }
    printDetail(e: React.SyntheticEvent<HTMLIFrameElement>) {
        e.currentTarget.contentWindow.focus();
        e.currentTarget.contentWindow.print();
    }
    valueChange(e: React.ChangeEvent<HTMLInputElement>) {
        this.setState({ [e.target.name]: e.target.value });
    }
    valueTextAreaChange(e: React.ChangeEvent<HTMLTextAreaElement>) {
        this.setState({ [e.target.name]: e.target.value });
    }
    itemUpdate(item: any) {
        this.setState({
            isUpdate: true,
            selectedItem: item,
            itemNewQuantity: item.qty,
            itemNewDiscount: item.discountPercentage,
            itemRemarks: item.remarks
        });
    }
    itemUpdateConfirm() {
        var thisClass = this;
        $.post('/Purchase/UpdateItem',
            {
                poid: this.state.selectedItem.purchaseOrderId,
                itemId: this.state.selectedItem.itemId,
                discount: this.state.itemNewDiscount,
                remarks: this.state.itemRemarks
            },
            data => {
                thisClass.setState({
                    data: data,
                    isUpdate: false,
                    newTax: data.taxPercentage,
                    otherRemarks: data.otherRemarks,
                    newOtherFees: data.otherFees,
                    newDiscount: data.discountPercentage,
                    globalRemarks: data.remarks
                });
                Materialize.toast('Item Updated!', 4000);
            });
    }
    updateConfirm() {
        var thisClass = this;
        $.post('/Purchase/Update',
            {
                poid: this.state.data.id,
                tax: this.state.newTax,
                discount: this.state.newDiscount,
                otherRemarks: this.state.otherRemarks,
                otherFees: this.state.newOtherFees,
                remarks: this.state.globalRemarks
            },
            data => {
                thisClass.setState({
                    data: data,
                    newTax: data.taxPercentage,
                    otherRemarks: data.otherRemarks,
                    newOtherFees: data.otherFees,
                    newDiscount: data.discountPercentage,
                    globalRemarks: data.remarks
                });
                Materialize.toast('Purchase Order Updated!', 4000);
            });


    }
    actionConfirm(id: number, actionId: number) {
        $.post('/Purchase/Action/' + id,
            {
                action: actionId
            },
            data => {
                if (!data.success)
                    Materialize.toast(data.message, 4000);
                else
                    window.location.href = '/Purchase';
            });
    }
    itemUpdateCancel() {
        this.setState({
            isUpdate: false
        });

    }
    componentDidMount() {
        $.post(this._url_Detail + '/' + this.orderId,
            data => {
                this.setState({
                    data: data,
                    newTax: data.taxPercentage,
                    otherRemarks: data.otherRemarks,
                    newOtherFees: data.otherFees,
                    newDiscount: data.discountPercentage,
                    globalRemarks: data.remarks
                });
            });
    }
    render() {
        return (
            <div>
                <iframe id="printf" name="printf" width="0" height="0" onLoad={(e) => this.printDetail(e)} title="Purchase Order Detail" hidden></iframe>
                <Link
                    to="/Purchase"
                    className="btn"><span className="fa fa-list"> </span> View List</Link>
                {this.state.data != null && !this.state.isUpdate
                    ?
                    <div>
                        <div className="row">
                            <h5>Purchase Order : {this.state.data.idString}</h5>
                            <h5>Status : {this.state.data.poStatus}</h5>
                        </div>
                        <div className="row">
                            <UserInfo user={this.state.data} />
                        </div>
                        <div className="row">
                            <table className="striped responsive-table">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Size</th>
                                        <th>Description</th>
                                        <th className="right-align">Price</th>
                                        <th className="right-align">Quantity</th>
                                        <th className="right-align">Total Price</th>
                                        <th className="right-align">Discount</th>
                                        <th className="right-align">Price Discount</th>
                                        <th className="right-align">Total</th>
                                        <th >Remarks</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {this.state.data.items.map((item: any, i: any) =>
                                        <tr key={item.itemId}>
                                            <td>{item.name}</td>
                                            <td>{item.size}</td>
                                            <td>{item.description}</td>
                                            <td className="right-align">{item.priceString}</td>
                                            <td className="right-align">{item.qty}</td>
                                            <td className="right-align">{item.totalPriceString}</td>
                                            <td className="right-align">{item.discountString}</td>
                                            <td className="right-align">{item.priceDiscountString}</td>
                                            <td className="right-align">{item.totalString}</td>
                                            <td >{item.remarks}</td>
                                            <td>
                                                {this.state.data.editable
                                                    ? <a className="btn btn-floating" onClick={() => this.itemUpdate(item)}><span className="fa fa-edit fa-2x"></span> </a>
                                                    : <span></span>
                                                }
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                        <div className="row">
                            <div className="card col s12 m4 l4">
                                <div className="card-content">
                                    <div className="title">
                                    </div>
                                    <dl>
                                        <dt><b>Total:</b></dt>
                                        <dd className="right-align">{this.state.data.totalString}</dd>
                                        <dt><b>Tax:</b>({this.state.data.taxString})</dt>
                                        <dd className="right-align"> (+) {this.state.data.priceTaxString}</dd>
                                        <dt><b>Other Fees:</b>({this.state.data.otherRemarks})</dt>
                                        <dd className="right-align"> (+) {this.state.data.otherFeesString}</dd>
                                        <dt><b>Transaction Discount:</b>({this.state.data.discountString})</dt>
                                        <dd className="right-align"> (-) {this.state.data.priceDiscountString}</dd>
                                        <dt><b>Grand Total:</b></dt>
                                        <dd className="right-align">{this.state.data.grandTotalString}</dd>
                                        <dt><b>Remarks:</b></dt>
                                        <dd className="right-align">{this.state.data.remarks}</dd>
                                        <span hidden={!this.state.data.editable}>
                                            <hr />
                                            UPDATE
                                            <dt><b>Tax(%):</b></dt>
                                            <dd><input type="number" name="newTax" min="0" max="100" defaultValue={this.state.data.taxPercentage} onChange={this.valueChange} /></dd>
                                            <dt><b>Other Fees Remark:</b></dt>
                                            <dd><input type="text" name="otherRemarks" defaultValue={this.state.data.otherRemarks} onChange={this.valueChange} /></dd>
                                            <dt><b>Other Fees:</b></dt>
                                            <dd><input type="number" name="newOtherFees" defaultValue={this.state.data.otherFees} onChange={this.valueChange} /></dd>
                                            <dt><b>Discount(%):</b></dt>
                                            <dd><input type="number" name="newDiscount" min="0" max="100" defaultValue={this.state.data.discountPercentage} onChange={this.valueChange} /></dd>
                                            <dt><b>Remarks:</b></dt>
                                            <dd>
                                                <textarea className="materialize-textarea" name="globalRemarks" defaultValue={this.state.data.remarks} onChange={this.valueTextAreaChange}>

                                                </textarea>
                                            </dd>
                                            <a className="btn" onClick={() => this.updateConfirm()}><span className="fa fa-edit"></span> Update</a>
                                        </span>
                                    </dl>


                                </div>
                            </div>
                        </div>
                        <div className="row">
                            <h5>Purchase Status : {this.state.data.poStatus}</h5>
                        </div>
                        <div className="row">
                            {this.state.data.actions.map((item: any, i: any) =>
                                <a className={item.className} key={item.id} onClick={() => this.actionConfirm(this.state.data.id, item.id)}><span className={item.classIcon}></span> {item.name}</a>
                            )}
                            <br />
                            {this.state.data.printable
                                ? <span>
                                    <a className="btn" onClick={() => $('#printf').attr('src', '/Purchase/DetailPrint/' + this.state.data.id)}><span className="fa fa-print"></span> Print PO</a>
                                    <a className="btn" onClick={() => $('#printf').attr('src', '/Purchase/InvDetailPrint/' + this.state.data.id)}><span className="fa fa-print"></span> Print Invoice</a>
                                </span>
                                : <span></span>
                            }
                        </div>
                    </div>
                    :
                    this.state.isUpdate
                        ?
                        <div>
                            <div className="row">
                                <UserInfo user={this.state.data} />
                            </div>
                            <div className="row">

                                <div className="col s12 m3 l4"></div>
                                <div className="col s12 m6 l4">
                                    <div className="card">
                                        <div className="card-content">
                                            <dl>
                                                <dt>Name:</dt>
                                                <dd>{this.state.selectedItem.name}</dd>
                                                <dt>Description:</dt>
                                                <dd>{this.state.selectedItem.description}</dd>
                                                <dt>Item Price:</dt>
                                                <dd>{this.state.selectedItem.priceString}</dd>
                                                <dt>Quantity:</dt>
                                                <dd>{this.state.selectedItem.qty}</dd>
                                                <dt>Discount:</dt>
                                                <dd>{this.state.selectedItem.discountString}</dd>
                                                <hr />
                                                <dt>New Discount(%):</dt>
                                                <dd><input type="number" name="itemNewDiscount" min="0" max="100" defaultValue={this.state.selectedItem.discountPercentage} onChange={this.valueChange} /></dd>
                                                <dt>Remarks:</dt>
                                                <dd><input type="text" name="itemRemarks" onChange={this.valueChange} defaultValue={this.state.selectedItem.remarks} /></dd>
                                            </dl>
                                        </div>
                                        <div className="card-action">
                                            <a className="btn" onClick={() => this.itemUpdateConfirm()}><span className="fa fa-edit"></span> Update</a>
                                            <a className="btn btn-red" onClick={() => this.itemUpdateCancel()}><span className="fa fa-times"></span> Cancel</a>
                                        </div>
                                    </div>
                                </div>
                                <div className="col s12 m3 l4"></div>
                            </div>
                        </div>
                        : <span>Not Available</span>}
            </div>
        );
    }
}