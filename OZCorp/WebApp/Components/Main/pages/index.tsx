import * as React from 'react';
import * as $ from 'jquery';

import {
    BrowserRouter,
    Route,
    Switch
} from 'react-router-dom';

import JobOrder from './page/jobOrder';
import JobOrderDetail from './page/jobOrderDetail';
import PageNotFound from './page/pageNotFound';
import Notification from './page/notification';
import Store from '../containers/store/Store';
import WarehouseItem from '../containers/warehouse/WarehouseItem';
import ManageItem from '../containers/product/itemTable';
import ManageUser from '../containers/manage/userTable';
import PoList from '../../PoListTable';
import MyCart from '../../MyCartTable';
import PurchaseOrderDetail from './page/purchaseOrderDetail';
import PurchaseReport from '../containers/report/purchaseReport';
import CompanyPurchaseReport from '../containers/report/companyReport';
import UserReport from '../containers/report/userReport';
import UserReportList from '../containers/report/userReportList';
import Home from '../containers/global/home';
import HomeSetting from '../containers/HomeSetting/homesetting';
import HomeSettingCreate from '../containers/HomeSetting/homesettingcreate';
import HomeSettingUpdate from '../containers/HomeSetting/homesettingupdate';
import HomeSettingRemove from '../containers/HomeSetting/homesettingremove';
import UserReportDetail from './page/userReportDetail';

export default class Layout extends React.Component<any, any>{
    requestNotification():void {
        $.post('/Common/NotificationCount',
            data => {
                if (data.counts > 0) {
                    $('.status-sitemap-75').empty().append(data.counts);
                    $('.status-sitemap-75').addClass('new badge');
                    $('.sitemap-75').addClass('red');
                } else {
                    $('.status-sitemap-75').empty();
                    $('.status-sitemap-75').removeClass('new badge');
                    $('.sitemap-75').removeClass('red');
                }
            });
    }
    componentDidMount(): void {
        this.requestNotification();
        setInterval(() => this.requestNotification()
            , 30000);
    }

    render() {
        return (<BrowserRouter>
                    <Switch>
                        <Route exact path="/" component={Home} />
                        <Route exact path="/Home/Index" component={Home} />
                        <Route exact path="/Manage/HomeSetting" component={HomeSetting} />
                        <Route exact path="/Manage/HomeSetting/Index" component={HomeSetting} />
                        <Route exact path="/Manage/HomeSetting/Create" component={HomeSettingCreate} />
                        <Route exact path="/Manage/HomeSetting/Update" component={HomeSettingUpdate} />
                        <Route exact path="/Manage/HomeSetting/Remove" component={HomeSettingRemove} />
                        <Route exact path="/Manage/Item" component={ManageItem} />
                        <Route exact path="/Manage/Item/Index" component={ManageItem} />
                        <Route exact path="/Manage/User" component={ManageUser} />
                        <Route exact path="/Manage/User/Index" component={ManageUser} />

                        <Route exact path="/Manage/Report/Company" component={CompanyPurchaseReport} />

                        <Route exact path="/Manage/Report" component={PurchaseReport} />
                        <Route exact path="/Manage/Report/Index" component={PurchaseReport} />

                        <Route exact path="/Store" component={Store} />
                        <Route exact path="/Store/Index" component={Store} />
                        <Route exact path="/JobOrder" component={JobOrder} />
                        <Route exact path="/JobOrder/Index" component={JobOrder} />

                        <Route exact path="/Warehouse" component={WarehouseItem} />
                        <Route exact path="/Warehouse/Index" component={WarehouseItem} />

                        <Route exact path="/JobOrder/Detail/:id" component={JobOrderDetail} />
                        <Route exact path="/Purchase/Detail/:id" component={PurchaseOrderDetail} />
                        <Route exact path="/Purchase" component={PoList} />
                        <Route exact path="/Purchase/Index" component={PoList} />
                        <Route exact path="/MyCart" component={MyCart} />
                        <Route exact path="/MyCart/Index" component={MyCart} />
                        <Route exact path="/Notification" component={Notification} />
                        <Route exact path="/Notification/Index" component={Notification} />
                        <Route exact path="/UserReport" component={UserReportList} />
                        <Route exact path="/UserReport/Index" component={UserReportList} />
                        <Route exact path="/UserReport/Create" component={UserReport} />
                        <Route exact path="/UserReport/Detail/:id" component={UserReportDetail} />
                        <Route component={PageNotFound} />
                    </Switch>
                </BrowserRouter>);
    }
    
}