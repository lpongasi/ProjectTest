
import * as React from 'react';
import { connect } from 'react-redux';
import * as $ from 'jquery';
import Dispatcher from '../../actions';
import { jobOrder } from '../../stateprops';
import CommonTable from '../../common/commontable';
import {
    Link
} from 'react-router-dom';
@connect(jobOrder, Dispatcher)
export default class JobOrders extends React.Component<any, any>{
    constructor(props) {
        super(props);
        this.printDetail = this.printDetail.bind(this);
    }
    componentWillMount(): void {
        $.post('/JobOrder/Detail',
            {
                id: this.props.jobId
            },
            data => {
                this.props.Dispatch('JO_DETAIL', data);
            });
    }
    printDetail(e: React.SyntheticEvent<HTMLIFrameElement>) {
        e.currentTarget.contentWindow.focus();
        e.currentTarget.contentWindow.print();
    }

    render() {
        var detail = this.props.jobOrder.joDetail;
        if (detail == null)
            return (
                <div>
                    <h4>Please Wait...</h4>
                </div>);

        return (
            <div>

                <Link
                    to="/JobOrder"
                    className="btn"><span className="fa fa-list"> </span> View List</Link>
                <iframe id="printf" name="printf" width="0" height="0" onLoad={(e) => this.printDetail(e)} title="Purchase Order Detail" hidden></iframe>
                <div className="row">
                    <h5>Job Order :{detail.idString} </h5>
                    <h6>Status :{detail.joStatus} </h6>
                </div>
                <div className="row">
                </div>
                <div className="row">
                    <table className="striped responsive-table">
                        <thead>
                            <tr>
                                <th>Barcode</th>
                                <th>Name</th>
                                <th>Size</th>
                                <th>Description</th>
                                <th className="right-align">Quantity</th>
                                <th>Status</th>
                                <th>Estimation</th>
                                <th>Date Started</th>
                                <th>Date Ended</th>
                                <th>Time Consumed</th>
                                <th>Estimation Diff</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                detail.items.map((item: any, i: any) =>
                                    <JoItemRow item={item} key={item.itemId} jobId={this.props.jobId} editable={detail.editable}/>
                                )
                            }
                        </tbody>
                    </table>
                </div>
                <div>
                    <a className="btn" onClick={() => $('#printf').attr('src', '/JobOrder/DetailPrint/' + this.props.jobId)}><span className="fa fa-print"></span> Print JO</a>
                </div>
            </div>
        );
    }
}
@connect(null, Dispatcher)
class JoItemRow extends React.Component<any, any> {
    constructor(props) {
        super(props);
        this.state = {
            day: this.props.item.estDay,
            hour: this.props.item.estHour,
            minute: this.props.item.estMinute
        }
        this.itemAction = this.itemAction.bind(this);
        this.itemSet = this.itemSet.bind(this);
        this.itemEstimate = this.itemEstimate.bind(this);
    }
    itemEstimate(item: string, value: string) {
        this.setState({
            [item]: value
        });
    }
    itemAction(itemId: number, actionId: any) {
        $.post('/JobOrder/ItemAction',
            {
                id: this.props.jobId,
                itemId,
                actionId
            },
            data => {
                if (data.success) {
                    this.props.Dispatch('JO_DETAIL', data.data);
                } else {
                    Materialize.toast(data.message, 4000);
                }
            });
    }

    itemSet(itemId: number) {
        $.post('/JobOrder/ItemSet',
            {
                id: this.props.jobId,
                itemId,
                day: this.state.day,
                hour: this.state.hour,
                minute: this.state.minute
            },
            () => {
                Materialize.toast('Item Estimated Time Set!', 4000);
            });
    }

    render(): Object {
        var item = this.props.item;

        var days = [];
        var hours = [];
        var minutes = [];
        for (var i = 0; i <= 60; i++) {
            days.push(<option key={i} value={i}>{i}</option>);
        }
        for (var i = 0; i <= 23; i++) {
            hours.push(<option key={i} value={i}>{i}</option>);
        }
        for (var i = 0; i <= 59; i++) {
            minutes.push(<option key={i} value={i}>{i}</option>);
        }

        return (
            <tr>
                <td>{item.barcode}</td>
                <td>{item.name}</td>
                <td>{item.size}</td>
                <td>{item.description}</td>
                <td className="right-align">{item.qty}</td>
                <td>{item.status}</td>
                {item.canEstimate && this.props.editable
                    ? <td>
                        <label>Day(s):</label>
                        <select className="browser-default" defaultValue={item.estDay} onChange={(ev) => this.itemEstimate('day', ev.target.value)}>
                            {days}
                        </select>
                        <label>Hour(s):</label>
                        <select className="browser-default" defaultValue={item.estHour} onChange={(ev) => this.itemEstimate('hour', ev.target.value)}>
                            {hours}
                        </select>
                        <label>Minute(s):</label>
                        <select className="browser-default" defaultValue={item.estMinute} onChange={(ev) => this.itemEstimate('minute', ev.target.value)}>
                            {minutes}
                        </select>
                        <a className="btn" onClick={() => this.itemSet(item.itemId)}><span className="fa fa-clock-o"> </span> SET</a>
                    </td>
                    : <td>
                        {item.estimatedTime}
                    </td>}
                <td>{item.startDate}</td>
                <td>{item.endDate}</td>
                <td>{item.timeConsumed}</td>
                <td>{item.estDiff}</td>
                <td>
                    {item.action != null && this.props.editable
                        ? <a className={item.action.className} onClick={() => this.itemAction(item.itemId, item.action.id)}><span className={item.action
                            .classIcon}></span> {item.action.name}</a>
                        : <span></span>
                    }
                </td>
            </tr>);
    }
}

