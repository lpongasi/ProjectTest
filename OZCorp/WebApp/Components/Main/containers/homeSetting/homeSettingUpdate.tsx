import * as React from 'react';
import {connect} from 'react-redux';
import Dispatcher from '../../actions';
import { homeSetting } from '../../stateprops';
import { Link } from 'react-router-dom';
@connect(homeSetting, Dispatcher)
export default class HomeSettingUpdate extends React.Component<any, any>{
    render() {
        if (this.props.homeSetting.update == null)
            return (
                    <div>
                        <h3>Not Available</h3>
                        <Link className="btn btn-white" to="/Manage/HomeSetting"><span className="fa fa-list"></span> Back to List</Link>
                    </div>
                );

        return (
            <div className="container row">
                <form id="formInputs" method="post" action={'/Manage/HomeSetting/Update/' +this.props.homeSetting.update.id} encType="multipart/form-data">
                    <div className="card">
                            <div className="card-title content-padding">
                                <h5><span className="fa fa-plus-square-o"></span>Update Slide</h5>
                            </div>
                            <div className="card-content">
                                <input type="hidden" defaultValue={this.props.homeSetting.update.id}/>
                                <div className="common-field">
                                    <label htmlFor="title">Title</label>
                                    <input type="text" name="title" id="title" defaultValue={this.props.homeSetting.update.title}/>
                                   
                                </div>
                                <div className="common-field">
                                    <label htmlFor="subTitle">Sub Title</label>
                                    <input type="text" name="subTitle" id="subTitle" defaultValue={this.props.homeSetting.update.subTitle}/>
                                    
                                </div>
                                <div className="input-field">
                                    <input type="checkbox" name="IsActive" id="IsActive" value="true" defaultChecked={this.props.homeSetting.update.isActive}/>
                                    <label htmlFor="IsActive">Active</label>
                                </div>
                                <div className="file-field input-field">
                                    <div className="btn">
                                        <span>Upload Image</span>
                                        <input type="file" id="imageUpload" name="imageUpload" accept="image/*"/>
                                    </div>
                                    <div className="file-path-wrapper">
                                        <input className="file-path validate" type="text"/>
                                    </div>
                                </div>
                            </div>
                            <div className="card-action">
                                    <button className="btn" type="submit"><span className="fa fa-plus-circle"></span> Update</button>
                                    <Link className="btn btn-white" to="/Manage/HomeSetting"> Cancel</Link>
                             </div>
                    </div>
                </form>
            </div>
        );
    }
}