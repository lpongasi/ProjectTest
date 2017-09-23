import * as React from 'react';
import { connect } from 'react-redux';
import Dispatcher from '../../actions';
import { manageItem } from '../../stateprops';
import CommonItem from '../../common/commonItem';
import CommonTable from '../../common/commontable';
@connect(manageItem, Dispatcher)
export default class ItemTable extends React.Component<any, any> {
    _url_Create: string = '/Manage/Item/Create';
    _url_Read: string = '/Manage/Item/List';
    _url_Update: string = '/Manage/Item/Edit';
    _url_Delete: string = '/Manage/Item/Delete';
    render() {
        return (
            <div className="content-padding-top">
                <h4><span className="fa fa-users"></span> Manage Items</h4>
                <div className="row col s12">
                    <a className="btn" href={this._url_Create}><i className="fa fa-plus-square"> </i> Create New</a>
                </div>
                <CommonTable readUrl={this._url_Read} dispatcherId="MANAGEITEM_LIST">
                    <div className="row">
                        {this.props.manageItem.list.map((item: any, i: any) =>
                            <CommonItem
                                key={item.id}
                                title={item.name}
                                subTitle={
                                    <span>
                                        Size: <strong>{item.size?item.size:'N/A'}</strong><br />
                                        Barcode: <strong>{item.barcode}</strong><br />
                                        Price: <strong>Php {item.priceString}</strong><br />
                                        Quantity Left: <strong>{item.qty} {item.uom}</strong><br />
                                    </span>
                                }
                                description={
                                    <span>
                                        {item.description}<br /><br />
                                        Barcode: {item.barcode}<br />
                                        Category: {item.category}<br />
                                        Price: {item.priceString}<br />
                                        Quantity Left: {item.qty} {item.uom}<br />
                                    </span>}
                                imageUrl={item.imageLocation.length > 0 ? item.imageLocation[0] : '/images/no-image.png'}
                                actionLinks={
                                    <span>
                                        <a href={this._url_Update + '/' + item.id} className="btn"> <span className="fa fa-edit"> </span>   Edit/Update</a>
                                        <a href={this._url_Delete + '/' + item.id} className="btn btn-red "> <span className="fa fa-times"> </span>  Remove</a>
                                    </span>
                                }
                            />
                        )}
                    </div>
                </CommonTable>
            </div >
        );
    }
}