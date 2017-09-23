import * as React from 'react';
import Dispatcher from '../../actions';
import { warehouse } from '../../stateprops';
import { connect } from 'react-redux';
import CommonItem from '../../common/commonItem';
import CommonTable from '../../common/commontable';
@connect(warehouse, Dispatcher)
export default class WarehouseItem extends React.Component<any, any> {
    _url_Read: string = '/Warehouse/List';
    render() {
        return (
            <div className="content-padding-top">
                <h4><span className="fa fa-building"></span> Warehouse</h4>
                <CommonTable readUrl={this._url_Read} dispatcherId="WAREHOUSE_LIST" >
                    <div className="row">
                        {this.props.warehouse.list.map((item: any, i: any) =>
                            <CommonItem
                                key={item.id}
                                title={item.name}
                                subTitle={
                                    <span>
                                        Size: <strong>{item.size?item.size:'N/A'}</strong> <br />
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
                                actionLinks={null}
                            />
                        )}
                    </div>
                </CommonTable>
            </div >
        );
    }
}