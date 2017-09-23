
import * as React from 'react';
import * as $ from 'jquery';
import { connect } from 'react-redux';
import Dispatcher from '../../actions';
import { homeSetting } from '../../stateprops';
import CommonItem from '../../common/commonItem';
import CommonTable from '../../common/commontable';
import {
    Link
    } from 'react-router-dom';


@connect(homeSetting, Dispatcher)
export default class HomeSetting extends React.Component<any, any>{
    render() {
        return (
            <div className="row">
                <h4><span className="fa fa-gears"> </span> Home Setting</h4>
                <Link className="btn" to="/Manage/HomeSetting/Create"> <span className="fa fa-plus"></span> Create New</Link>
                <CommonTable readUrl="/Manage/HomeSetting/List" dispatcherId="HOMESETTING_LIST" >
                    <div className="row">
                        {this.props.homeSetting.list.map((item: any, i: any) =>
                            <CommonItem
                                key={item.id}
                                title={item.title}
                                subTitle={
                                    <span>
                                       {item.subTitle}<br/>
                                        Active:<strong>{item.isActive?'YES':'NO'}</strong>
                                    </span>
                                }
                                description={
                                    <span>
                                        {item.subTitle}<br /><br />
                                        Active:<strong>{item.isActive?'YES':'NO'}</strong>
                                    </span>}
                                imageUrl={item.fileLocation?item.fileLocation : '/images/no-image.png'}
                                actionLinks={
                                       <span>
                <Link className="btn" to="/Manage/HomeSetting/Update" onClick={()=>this.props.Dispatch('HOMESETTING_UPDATE',item)} > <span className="fa fa-edit"></span></Link>
                <Link className="btn btn-red" to="/Manage/HomeSetting/Remove" onClick={()=>this.props.Dispatch('HOMESETTING_REMOVE',item)} > <span className="fa fa-times"></span></Link>
                                            
                                       </span>
                                }
                            />
                        )}
                    </div>
                </CommonTable>
            </div>
        );
    }
}