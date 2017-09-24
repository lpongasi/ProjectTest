
import * as React from 'react';
import * as $ from 'jquery';
import { connect } from 'react-redux';
import Dispatcher from '../../actions';
import { homeSetting } from '../../stateprops';
import {
    Link
    } from 'react-router-dom';

@connect(homeSetting, Dispatcher)
export default class Home extends React.Component<any, any>{
    componentDidMount(): void {
        $.post('/Manage/HomeSetting/Ads', null, result => this.props.Dispatch('HOMESETTING_ADS', result)).done(() => $('.slider').slider());
        $.post('/Manage/HomeSetting/CanEdit', null, result => this.props.Dispatch('HOMESETTING_CANEDIT', result.success));
    }
    render() {
        var align = ['center-align', 'left-align', 'right-align'];
        return (
            <div>
                {this.props.homeSetting.edit
                ?<div className="fixed-action-btn">
                        <Link to="/Manage/HomeSetting" className="btn-floating btn-large yellow waves-circle waves-effect">
                            <i className="fa fa-edit fa-2x black-text"></i>
                        </Link>
                    </div>
                    :<span></span>}
                <div className="slider fullscreen">
                    <ul className="slides">
                        {this.props.homeSetting.listAds.map((item: any, i: any) =>
                            <li key={item.id}>
                                <img src={item.fileLocation} alt={item.title} />
                                <div className={'caption ' + align[(i + 1) % 3]}>
                                    <h3 className="black-text stroke-base">{item.title}</h3>
                                    <h5 className="black-text stroke-base">{item.subTitle}</h5>
                                </div>
                            </li>)}
                    </ul>
                </div>
            </div>
        );
    }
}