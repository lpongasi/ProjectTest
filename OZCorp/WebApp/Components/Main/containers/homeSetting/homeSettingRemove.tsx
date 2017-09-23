import * as React from 'react';
import {connect} from 'react-redux';
import Dispatcher from '../../actions';
import { homeSetting } from '../../stateprops';
import { Link } from 'react-router-dom';
@connect(homeSetting, Dispatcher)
export default class HomeSettingRemove extends React.Component<any, any>{
    render() {
        if (this.props.homeSetting.remove == null)
            return (
                <div>
                    <h3>Not Available</h3>
                    <Link className="btn btn-white" to="/Manage/HomeSetting"><span className="fa fa-list"></span> Back to List</Link>
                </div>
            );
        return (
            <div className="container row">
                <form id="formInputs" method="post" action={'/Manage/HomeSetting/Remove/' + this.props.homeSetting.remove.id}>
                    <div className="card">
                            <div className="card-title content-padding">
                                <h5><span className="fa fa-times-circle-o"></span> Remove Slide?</h5>
                            </div>
                            <div className="card-content">
                                <input type="hidden" defaultValue={this.props.homeSetting.remove.id}/>
                                Title: <strong>{this.props.homeSetting.remove.title}</strong><br/>
                                SubTitle: <strong>{this.props.homeSetting.remove.subTitle}</strong><br/>
                                Active: <strong>{this.props.homeSetting.remove.isActive?'YES':'NO'}</strong><br/>
                            </div>
                            <div className="card-action">
                                    <button className="btn btn-red" type="submit"><span className="fa fa-plus-circle"></span> Remove</button>
                                    <Link className="btn btn-white" to="/Manage/HomeSetting"> Cancel</Link>
                             </div>
                    </div>
                </form>
            </div>
        );
    }
}