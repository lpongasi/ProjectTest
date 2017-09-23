
import * as React from 'react';
import { connect } from 'react-redux';
import * as $ from 'jquery';
import Dispatcher from '../../actions';
import { notification } from '../../stateprops';
import CommonTable from '../../common/commontable';
import {
    Link
} from 'react-router-dom';


@connect(notification, Dispatcher)
export default class Notification extends React.Component<any, any>{

    render() {
        return (
            <div>
                <CommonTable readUrl="/Common/NotificationList" dispatcherId="NOTIFY_FILTER" >
                    <table className="striped highlight responsive-table">
                        <thead>
                            <tr>
                                <th></th>
                                <th>Title</th>
                                <th>Date</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.notification.list.map((item: any, i: any) =>
                                <tr key={item.id}>
                                    <td>
                                        <span className={item.isViewed ? 'fa fa-bell-o fa-2x' : 'fa fa-bell fa-2x'}></span>
                                    </td>
                                    <td>{item.title}</td>
                                    <td>{item.notifDate}</td>
                                    <td>
                                        <a
                                            onClick={() => $.post('/Common/NotifViewed/' + item.id)}
                                            href={item.url}
                                            className="btn btn-floating"><span className="fa fa-list"> </span> </a>
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </CommonTable>
            </div>
        );
    }
}