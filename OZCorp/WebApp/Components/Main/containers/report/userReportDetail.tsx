import * as React from 'react';
import { connect } from 'react-redux';
import * as $ from 'jquery';
import Dispatcher from '../../actions';
import { report } from '../../stateprops';
import {
    Link
} from 'react-router-dom';

@connect(report, Dispatcher)
export default class UserReportDetail extends React.Component<any, any>{
    constructor(props) {
        super(props);
        this.formSubmit = this.formSubmit.bind(this);
    }
    componentWillMount(): void {
        $.post('/UserReport/ReportDetail/' + this.props.repId,
            data => {
                this.props.Dispatch('REPORT_DETAIL', data);
            });
    }
    unlock() {
        $.post('/UserReport/Unlock',
            {
                id: this.props.repId
            }, data => {
                if (data.success)
                    window.location.href = '/UserReport';
                else
                    Materialize.toast(data.message, 8000);
            });
    }

    formSubmit(action: string) {
        $.post('/UserReport/ReportUpdate',
            {
                id: this.props.repId,
                action,
                data: $('#reportForm').serializeArray()
            }, data => {
                if (data.success)
                    window.location.href = '/UserReport';
                else
                    Materialize.toast(data.message, 8000);
            });
    }
    render() {
        if (this.props.report.detail == null)
            return (<div></div>);
        console.log(this.props.report.detail);
        return (
            <div className="container">
                <Link
                    to="/UserReport"
                    className="btn"><span className="fa fa-list-alt"> </span> List of Reports</Link>
                <div className="row">
                    <div className="card">
                        <div className="card-content">
                            <span className="pull-right">Date: {this.props.report.detail.date}</span>
                            <br />Company Address: {this.props.report.detail.companyAddress}
                            <br />Company Contact: {this.props.report.detail.companyContact}
                            <br />Company Name: {this.props.report.detail.companyName}
                            <br />Full Name: {this.props.report.detail.fullName}
                            <br />Email: {this.props.report.detail.email}
                        </div>
                    </div>
                </div>
                <form method="post" id="reportForm">
                    {this.props.report.detail.list.map((item, i) =>
                        <div className="row" key={i}>
                            {item.isField
                                ?
                                this.props.report.detail.isLock ?
                                    <h6>{item.title}: <b>{item.valueString}</b></h6>
                                    : <div className="common-field col s12">
                                        <label htmlFor={item.id} className="black-text">
                                            {!item.parentId != null ?
                                                <span>
                                                    {item.title}
                                                </span>
                                                : <strong>
                                                    {item.title}
                                                </strong>}
                                        </label>
                                        <input id={item.id} name={item.id} type="number" defaultValue={item.value} />
                                    </div>
                                : <h4>{item.title}</h4>
                            }
                        </div>
                    )}
                    {!this.props.report.detail.isLock ?
                        <span>
                            <button type="button" className="btn" onClick={() => this.formSubmit('save')}><span className="fa fa-save"></span> Save</button>
                            <button type="button" className="btn btn-green" onClick={() => this.formSubmit('lock')}><span className="fa fa-lock"></span> Save and Lock</button>
                        </span>
                        :
                        this.props.report.detail.isAdmin
                            ? <span>
                                <button type="button" className="btn" onClick={this.unlock.bind(this)}><span className="fa fa-unlock"></span> Unlock</button>

                            </span>
                            : <span></span>}
                </form>
            </div>
        );
    }
}