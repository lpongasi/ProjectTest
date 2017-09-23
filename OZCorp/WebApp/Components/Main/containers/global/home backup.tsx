
import * as React from 'react';
import * as $ from 'jquery';
import { connect } from 'react-redux';
import Media from 'react-media';
import Dispatcher from '../../actions';
import { homeSetting } from '../../stateprops';
import {
    Link
} from 'react-router-dom';

@connect(homeSetting, Dispatcher)
export default class Home extends React.Component<any, any>{
    constructor(props) {
        super(props);
        this.createImage = this.createImage
    }
    componentDidMount(): void {
        //$.post("/Manage/HomeSetting/Ads", null, result => this.props.Dispatch("HOMESETTING_ADS", result)).done(() => $('.slider').slider());
        //$.post("/Manage/HomeSetting/CanEdit", null, result => this.props.Dispatch("HOMESETTING_CANEDIT", result.success));

        $('.box-sortable').sortable();
    }
    createImage(imageUrl: string): object {
        return {
            background: "url('/images/" + imageUrl + "') no-repeat",
            WebkitBackgroundSize: 'cover',
            MozBackgroundSize: 'cover',
            OBackgroundSize: 'cover',
            BackgroundSize: 'cover'
        };
    }
    render() {
        return (
            <ul className="box box-sortable">
                <li className="box-item wide">
                    <div className="content-wrapper" style={this.createImage('w1.jpg')}>
                        <div className="content">
                            <img src="/images/oz.png" />
                            TEST
                        </div>
                    </div>
                </li>
                <li className="box-item medium">
                    <div className="content-wrapper" style={this.createImage('sq1.jpg')}>
                        <div className="content">
                            <img src="/images/oz.png" />
                            WE TEST
                        </div>
                    </div>

                </li>
                <li className="box-item medium">
                    <div className="content-wrapper" style={this.createImage('sq2.jpg')}>
                        <div className="content">
                            HE TEST
                        </div>
                    </div>

                </li>
                <li className="box-item tall">
                    <div className="content-wrapper" style={this.createImage('w1.jpg')}>
                        <div className="content">
                            WERE TEST
                        </div>
                    </div>
                </li>
                <li className="box-item box-group">
                    <div className="content-wrapper">
                        <ul className="box box-sortable">
                            <li className="box-item medium">
                                <div className="content-wrapper" style={this.createImage('sq1.jpg')}>
                                    <div className="content">
                                        WERE TEST
                                    </div>
                                </div>
                            </li>
                            <li className="box-item medium">
                                <div className="content-wrapper" style={this.createImage('sq2.jpg')}>
                                    <div className="content">
                                        WERE TEST
                                    </div>
                                </div>
                            </li>
                            <li className="box-item medium">
                                <div className="content-wrapper">
                                    <div className="content">
                                        WERE TEST
                                    </div>
                                </div>
                            </li>
                            <li className="box-item medium">
                                <div className="content-wrapper">
                                    <div className="content">
                                        WERE TEST
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </li>
                <li className="box-item box-group">
                    <div className="content-wrapper">
                        <ul className="box box-sortable">
                            <li className="box-item wide">
                                <div className="content-wrapper">
                                    <div className="content">
                                        WERE TEST
                                    </div>
                                </div>
                            </li>
                            <li className="box-item medium">
                                <div className="content-wrapper">
                                    <div className="content">
                                        WERE TEST
                                    </div>
                                </div>
                            </li>
                            <li className="box-item medium">
                                <div className="content-wrapper">
                                    <div className="content">
                                        WERE TEST
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </li>
                <li className="box-item box-group">
                    <div className="content-wrapper">
                        <ul className="box box-sortable">
                            <li className="box-item tall">
                                <div className="content-wrapper">
                                    <div className="content">
                                        WERE TEST
                                    </div>
                                </div>
                            </li>
                            <li className="box-item medium">
                                <div className="content-wrapper">
                                    <div className="content">
                                        WERE TEST
                                    </div>
                                </div>
                            </li>
                            <li className="box-item medium">
                                <div className="content-wrapper">
                                    <div className="content">
                                        WERE TEST
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                </li>
            </ul>
        );
    }
}
