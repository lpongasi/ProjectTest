
import * as React from 'react';
import * as $ from 'jquery';
import { connect } from 'react-redux';
import Media from 'react-media';
import Dispatcher from '../../actions';
import {Box, BoxItem, BoxBig, BoxTall, BoxWide, BoxGroup, BoxMedium } from '../box';
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
    render() {
        return (
            <Box>
                <BoxItem BoxType={BoxBig}>
                </BoxItem>
                <BoxItem BoxType={BoxGroup}>
                    <BoxItem BoxType={BoxTall}>
                    </BoxItem>
                    <BoxItem BoxType={BoxMedium}>
                    </BoxItem>
                    <BoxItem BoxType={BoxMedium}>
                    </BoxItem>
                </BoxItem>
                <BoxItem BoxType={BoxGroup}>
                    <BoxItem BoxType={BoxMedium}>
                    </BoxItem>
                    <BoxItem BoxType={BoxMedium}>
                    </BoxItem>
                    <BoxItem BoxType={BoxWide}>
                    </BoxItem>
                </BoxItem>
                <BoxItem BoxType={BoxTall}>
                </BoxItem>
                <BoxItem BoxType={BoxMedium}>
                </BoxItem>
                <BoxItem BoxType={BoxMedium}>
                </BoxItem>             
            </Box>
        );
    }
}
