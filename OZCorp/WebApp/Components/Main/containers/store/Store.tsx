import * as React from 'react';
import * as $ from 'jquery';
import { StoreTable } from '../../../Component/Table/StoreTable';
import CommonItem from '../../common/commonItem';

export default class Store extends React.Component<any, any> {
    _url_Read: string = '/Store/List';
    _url_AddCart: string = '/Store/AddToCart';
    _url_Remove: string = '/Store/RemoveItem';
    constructor(props: any) {
        super(props);
        this.state = {
            data: [],
            selectedItem: null,
            totalValue: 0
        };
        this.dataReturn = this.dataReturn.bind(this);
        this.addToCart = this.addToCart.bind(this);
        this.selectedItemNull = this.selectedItemNull.bind(this);
        this.inputChange = this.inputChange.bind(this);
        this.cartAdd = this.cartAdd.bind(this);
        this.removeItem = this.removeItem.bind(this);
    }
    selectedItemNull() {
        this.setState({
            selectedItem: null
        });
    }

    removeItem(item: any) {
        $.post(this._url_Remove, {
            id: item.id
        }, data => {
            if (data.success) {
                this.selectedItemNull();
            } else {
                Materialize.toast(data.message, 4000);
            }
        },
            'json'
        ).fail(() => {
            Materialize.toast('Error Found! Please contact your system administrator!', 4000);
        });
    }
    addToCart(item: any) {
        $('#itemQty').val(1);
        this.setState({
            selectedItem: item,
            totalValue: item.price * 1
        },()=> $('.materialboxed').materialbox());
    }
    cartAdd() {
        $.post(this._url_AddCart, {
            itemId: this.state.selectedItem.id,
            quantity: $('#itemQty').val()
        }, data => {
            if (data.success) {
                Materialize.toast('Item Added To Cart!', 4000);
                this.selectedItemNull();
            } else {
                Materialize.toast(data.message, 4000);
            }
        },
            'json'
        ).fail(() => {
            Materialize.toast('Error Found! Please contact your system administrator!', 4000);
        });
    }
    inputChange() {
        this.setState({
            totalValue: this.state.selectedItem.price * $('#itemQty').val()
        });
    }
    dataReturn(data: any[]) {
        this.setState({ data: data });
    }
    componentDidMount(): void {
        $('.materialboxed').materialbox();
    }
    render() {
        return (
            <div className="content-padding-top">
                <h4><span className="fa fa-shopping-bag"></span> Store</h4>
                {this.state.selectedItem != null
                    ? <div className="container">
                        <div className="card">
                            <div className="card-content">
                                <div className="card-image">
                                    <img className="materialboxed" src={this.state.selectedItem.images.length > 0 ? this.state.selectedItem.images[0].fileLocation.replace('.', '-Orig.') : '/images/no-image.png'} />
                                </div>
                                <div className="row">
                                    <h6 className="col">
                                        <strong className="truncate">{this.state.selectedItem.name}</strong>
                                        Size:<br /> <strong>{this.state.selectedItem.size?this.state.selectedItem.size:'N/A'}</strong><br />
                                        Barcode:<br /> <strong className="truncate"><span className="fa fa-barcode"></span> {this.state.selectedItem.barcode}</strong>
                                        Price:<br /> <strong><span className="fa fa-money"></span> Php {this.state.selectedItem.priceString}</strong><br />
                                        Quantity Left:<br /> <strong className="truncate">{this.state.selectedItem.qty} {this.state.selectedItem.uom}</strong>
                                        Status: <strong className="truncate">{this.state.selectedItem.qty <= 0 && !this.state.selectedItem.purPro ? 'Out of Stock!' : 'Available'}</strong>
                                    </h6>
                                </div>
                                {this.state.selectedItem.qty <= 0 && !this.state.selectedItem.purPro
                                    ? <span></span>
                                    : <div className="row">
                                        <div className="col s12">
                                            <div className="common-field">
                                                <label htmlFor="itemQty">Quantity</label>
                                                <input id="itemQty" type="number" min="1" defaultValue="1" max="999999" onChange={this.inputChange} />
                                            </div>
                                            <h6>Total : {this.state.totalValue}</h6>
                                        </div>
                                    </div>}
                            </div>
                            <div className="card-action">
                                {this.state.selectedItem.qty <= 0 && !this.state.selectedItem.purPro?<span></span>
                                    : <a className="btn" onClick={this.cartAdd}><span className="fa fa-cart-plus"></span> Add to cart</a> }
                                
                                <a className="btn btn-white" onClick={this.selectedItemNull}><span className="fa fa-times-rectangle"></span> Cancel</a>
                            </div>
                        </div>
                    </div>
                    :
                    <StoreTable readUrl={this._url_Read} dataReturn={this.dataReturn} filter={null}>
                        <div className="row">
                            {this.state.data.map((item: any, i: any) =>
                                <div className="col s6 m3 l2" key={item.id} onClick={() => this.addToCart(item)}>
                                    <div className="card">
                                        <div className="card-image">
                                            <img src={item.images.length > 0 ? item.images[0].fileLocation : '/images/no-image.png'} />
                                        </div>
                                        <div className="card-content padding-all-5">
                                            <h6>
                                                <strong className="truncate">{item.name}</strong>
                                                <strong><span className="fa fa-money"></span> Php {item.priceString}</strong><br />
                                               <strong className="truncate">{item.qty} {item.uom}</strong>
                                               <strong className="truncate">Size: {item.size ? item.size:'N/A'}</strong>
                                            </h6>
                                        </div>
                                    </div>
                                </div>
                            )}
                        </div>
                    </StoreTable>
                }
            </div >
        );
    }
}