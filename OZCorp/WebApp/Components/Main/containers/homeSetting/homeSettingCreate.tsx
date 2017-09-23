import * as React from 'react';
import {
    Link
    } from 'react-router-dom';

export default class HomeSettingCreate extends React.Component<any, any>{
    render() {
        return (
            <div className="container row">
                <form id="formInputs" method="post" action="/Manage/HomeSetting/Create" encType="multipart/form-data">
                    <div className="card">
                            <div className="card-title content-padding">
                                <h5><span className="fa fa-plus-square-o"></span> Create New Slide</h5>
                            </div>
                            <div className="card-content">
                                <div className="input-field common-field">
                                    <input type="text" name="title" id="title"/>
                                    <label htmlFor="title">Title</label>
                                </div>
                                <div className="input-field common-field">
                                    <input type="text" name="subTitle" id="subTitle"/>
                                    <label htmlFor="subTitle">Sub Title</label>
                                </div>
                                <div className="input-field">
                                    <input type="checkbox" name="isActive" id="isActive"/>
                                    <label htmlFor="isActive">Active</label>
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
                                    <button className="btn" type="submit"><span className="fa fa-plus-circle"></span> Create</button>
                                    <Link className="btn btn-white" to="/Manage/HomeSetting"> Cancel</Link>
                             </div>
                    </div>
                </form>
            </div>
        );
    }
}