import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as $ from 'jquery';
import { CommonTable } from './Component/Table/CommonTable';
const elementId = 'itemList';
export default class MyCartTable extends React.Component<any, any> {
    _url_Read: string = $('#' + elementId).attr('url-read');
    _url_Update: string = $('#' + elementId).attr('url-update');
    constructor(props: any) {
        super(props);
        this.state = {
            data: [],
            dataPreview: [],
            dataSource: [],
            totalPrice: $('#' + elementId).attr('data-currentTotal'),
            message: null,
            actionClick: false,
            purchasePreview: false,
            isUpdateItem: false,
            itemToUpdate: null
        };
        this.dataReturn = this.dataReturn.bind(this);
        this.itemSelect = this.itemSelect.bind(this);
        this.selectAll = this.selectAll.bind(this);
        this.purchase = this.purchase.bind(this);
        this.purchasePreview = this.purchasePreview.bind(this);
        this.itemUpdate = this.itemUpdate.bind(this);
        this.updateItem = this.updateItem.bind(this);
        this.removeItem = this.removeItem.bind(this);
    }
    dataReturn(data: any[]) {
        this.setState({ data: data });
    }
    itemUpdate(item: any) {
        this.setState({
            isUpdateItem: true,
            itemToUpdate: item
        });
    }
    updateItem(itemId: number) {
        var thisState = this;
        $.post('/MyCart/UpdateItem', {
            itemId: itemId,
            quantity: $('#updatedQuantity').val()
        }, data => {
            if (data.success) {
                thisState.setState({
                    isUpdateItem: false,
                    totalPrice: data.data
                });
            } else {
                Materialize.toast(data.message, 4000);
            }
        });
    }
    removeItem(itemId: number) {
        var thisState = this;
        $.post('/MyCart/RemoveItem', {
            itemId: itemId
        }, data => {
            if (data.success) {
                thisState.setState({
                    isUpdateItem: false,
                    totalPrice: data.data
                });
            } else {
                Materialize.toast(data.message, 4000);
            }
        });
    }
    componentDidUpdate(nextProps: any, nextState: any) {
        $('.tooltipped').tooltip();
        $('#selectAllbtn').prop('checked', false);
    }

    componentDidMount() {
        $('.tooltipped').tooltip();
    }
    itemSelect(e: any, itemId: any) {
        var action = $(e.target).is(':checked');
        var thisState = this;
        $.post('/MyCart/ItemSelect', {
            id: itemId,
            action: action
        }, data => {
            if (data.success) {
                $('#selectAllbtn').prop('checked', false);
                thisState.setState({
                    totalPrice: data.data
                });
            }
        });
    }

    selectAll(e: any, itemId: any) {
        var action = $(e.target).is(':checked');
        $.post('/MyCart/ItemSelect', {
            id: itemId,
            action: action
        }, data => {
            if (data.success) {
                $('.checklist').prop('checked', action);
                $('.totalInCart').empty().append(data.data);
            }
        });
    }
    purchase(action: any) {
        var thisClass = this;
        thisClass.setState({
            actionClick: true
        });
        $.post('/MyCart/Purchase', {
            action: action
        }, data => {
            if (data.success) {
                window.location.href = data.data;
            }
            thisClass.setState({
                message: data.message,
                actionClick: false
            });
        });

    }
    purchasePreview() {
        var thisClass = this;
        $.post('/MyCart/PurchaseList'
            , data => {
                if (data.data.length > 0) {
                    thisClass.setState({
                        dataPreview: data.data,
                        purchasePreview: true,
                        message: ''
                    });
                } else {
                    thisClass.setState({
                        message: 'No selected item found!'
                    });
                }
            });
    }
    render() {
        return (
            <div className="content-padding-top">
                {this.state.isUpdateItem ?
                    <div>
                        <div className="row">
                            <div className="col s12 m3 l3"></div>
                            <div className="col s12 m6 m6">
                                <h5>{this.state.itemToUpdate.name}</h5>
                                <div className="card">
                                    <div className="card-content">
                                        <dl>
                                            <dt>Description:</dt>
                                            <dd className="flow-text">{this.state.itemToUpdate.description}</dd>
                                            <dt>Quantity:</dt>
                                            <dd>{this.state.itemToUpdate.quantity}</dd>
                                            <dt>Quantity Left:</dt>
                                            <dd>{this.state.itemToUpdate.quantityLeft}</dd>
                                            <dt>Price:</dt>
                                            <dd>{this.state.itemToUpdate.priceString}</dd>
                                        </dl>
                                        <dl>
                                            <dt>Total Price:</dt>
                                            <dd id="totalPrice">{this.state.itemToUpdate.totalString}</dd>
                                        </dl>
                                        <div className="common-field">
                                            <label htmlFor="updatedQuantity">New Quantity</label>
                                            <input id="updatedQuantity" defaultValue={this.state.itemToUpdate.quantity} />
                                        </div>
                                    </div>
                                    <div className="card-action">
                                        <a className="btn" onClick={() => this.updateItem(this.state.itemToUpdate.itemId)}><span className="fa fa-edit"></span> Update</a>
                                        <a className="btn btn-red" onClick={() => this.removeItem(this.state.itemToUpdate.itemId)}><span className="fa fa-times"></span> Remove</a>
                                        <a className="btn btn-green" onClick={() => this.setState({ isUpdateItem: false })}><span className="fa fa-times"></span> Cancel</a>
                                    </div>
                                </div>
                            </div>
                            <div className="col s12 m3 l3"></div>
                        </div>
                    </div>
                    : <div>
                        <div className="red-text left-align">{this.state.message}</div>
                        <div className="right-align">
                            <b><i>Total Price in Cart:</i> <u className="totalInCart">{this.state.totalPrice}</u></b>
                        </div>
                        {this.state.purchasePreview
                            ?
                            <table className="striped highlight responsive-table">
                                <thead>
                                    <tr>
                                        <th>Name</th>
                                        <th>Description</th>
                                        <th className="right-align">Price</th>
                                        <th className="right-align">Quantity</th>
                                        <th>Unit of Measure</th>
                                        <th className="right-align">Total</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {this.state.dataPreview.map((item: any, i: any) =>
                                        <tr key={item.itemId}>
                                            <td>{item.name}</td>
                                            <td>{item.description}</td>
                                            <td className="right-align">{item.priceString}</td>
                                            <td className="right-align">{item.quantity}</td>
                                            <td>{item.unitOfMeasure}</td>
                                            <td className="right-align">{item.totalString}</td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                            : <CommonTable readUrl={this._url_Read} dataReturn={this.dataReturn}>
                                <table className="striped highlight">
                                    <thead>
                                        <tr>
                                            <th className="right-align" width="60">
                                                <input type="checkbox" key="selectAllKey" className="filled-in checklist" id="selectAllbtn" onChange={(e) => this.selectAll(e, this.state.data.map((item: any, i: any) => item.itemId).join(','))} />
                                                <label htmlFor="selectAllbtn"></label>
                                            </th>
                                            <th></th>
                                            <th>Name</th>
                                            <th>Description</th>
                                            <th className="right-align">Price</th>
                                            <th className="right-align">Quantity Purchase</th>
                                            <th className="right-align">Quantity Left</th>
                                            <th>Unit of Measure</th>
                                            <th className="right-align">Total</th>
                                            <th className="right-align">Status</th>
                                            <th width="80"></th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {this.state.data.map((item: any, i: any) =>
                                            <tr key={item.itemId}>
                                                <td className="right-align">
                                                    <input type="checkbox" className="filled-in checklist" id={item.itemId} defaultChecked={item.selected} onChange={(e) => this.itemSelect(e, item.itemId)} />
                                                    <label htmlFor={item.itemId}></label>
                                                </td>
                                                <td>
                                                    <img src={item.imageLocation.length > 0 ? item.imageLocation[0] : '/images/no-image.png'} className="table-image" />

                                                </td>
                                                <td>{item.name}</td>
                                                <td>{item.description}</td>
                                                <td className="right-align">{item.priceString}</td>
                                                <td className="right-align">{item.quantity}</td>
                                                <td className="right-align">{item.quantityLeft}</td>
                                                <td>{item.unitOfMeasure}</td>
                                                <td className="right-align">{item.totalString}</td>
                                                <td className="right-align">{item.status}</td>
                                                <td>
                                                    <a className="btn btn-floating" onClick={() => this.itemUpdate(item)}><span className="fa fa-edit fa-2x"></span> </a>
                                                </td>
                                            </tr>
                                        )}
                                    </tbody>
                                </table>
                            </CommonTable>
                        }
                        <hr />
                        <div className="right-align">
                            <h5><i>Total Price in Cart:</i> <u className="totalInCart">{this.state.totalPrice}</u></h5>
                        </div>
                        <div className="red-text left-align">{this.state.message}</div>


                        {this.state.actionClick ? ''
                            : <div className="row right-align">
                                {!this.state.purchasePreview
                                    ?
                                    <span>
                                        <a onClick={() => this.purchasePreview()} className="btn"><span className="fa fa-money"></span> Purchase</a>
                                        <a onClick={() => this.purchase('clear')} className="btn btn-red"><span className="fa fa-times-rectangle"></span> Clear Cart</a>
                                    </span>
                                    :
                                    <span>
                                        <h3>Please Confirm!</h3>
                                        <a onClick={() => this.purchase('purchase')} className="btn"><span className="fa fa-money"></span> Purchase</a>
                                        <a onClick={() => this.purchase('preserve')} className="btn btn-green"><span className="fa fa-cart-plus"></span> Purchase and Preserve Cart</a>
                                        <a onClick={() => this.setState({ purchasePreview: false })} className="btn btn-red"><span className="fa fa-times-rectangle"></span> Cancel</a>
                                    </span>

                                }
                            </div>}
                    </div>}
            </div>
        );
    }
}