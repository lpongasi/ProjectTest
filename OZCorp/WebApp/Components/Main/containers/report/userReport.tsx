import * as React from 'react';
import { connect } from 'react-redux';
import * as $ from 'jquery';
import Dispatcher from '../../actions';
import { report } from '../../stateprops';
import {
    Link
    } from 'react-router-dom';

@connect(report, Dispatcher)
export default class UserReport extends React.Component<any, any>{
    constructor(props) {
        super(props);
        this.formSubmit = this.formSubmit.bind(this);
    }
    componentWillMount(): void {
        $.post('/UserReport/List',
            data => {
                this.props.Dispatch('REPORT_LIST', data);
            });
    }
    formSubmit(action: string) {
        $.post('/UserReport/Report',
            {
                month: $('#reportMonth').val(),
                year: $('#reportYear').val(),
                action,
                data: $('#reportForm').serializeArray()
            }, data => {
                if(data.success)
                    window.location.href = '/UserReport';
                else
                    Materialize.toast(data.message,8000);
            });
    }
    render() {
        var date = new Date();
        var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
        var year = [];
        for (var i = date.getFullYear() - 10; i <= date.getFullYear(); i++) {
            year.push(i);
        }
        return (
            <div className="container">
                <Link
                    to="/UserReport"
                    className="btn"><span className="fa fa-list-alt"> </span> List of Reports</Link>

                <div className="row">
                    <div className="col s12 m6 l6">
                        <label>Month</label>
                        <select className="browser-default" id="reportMonth">
                            {months.map((m, i) =>
                                <option key={i} value={i + 1}>{m}</option>
                            )}
                        </select>
                    </div>
                    <div className="col s12 m6 l6">
                        <label>Year</label>
                        <select className="browser-default" id="reportYear">
                            {year.map((m, i) =>
                                <option key={i} value={m}>{m}</option>
                            )}
                        </select>
                    </div>
                </div>
                <form method="post" id="reportForm">
                    {this.props.report.list.map((item, i) =>
                        <div className="row" key={item.id}>
                            {item.isField
                                ? <div className="input-field common-field col s12">
                                    <label htmlFor={item.id} className="black-text">
                                        {!item.parentId != null ?
                                            <span>
                                                {item.name}
                                            </span>
                                            : <strong>
                                                {item.name}
                                            </strong>}
                                    </label>
                                    <input id={item.id} name={item.id} type="number" />
                                </div>
                                : <h4>{item.name}</h4>
                            }
                        </div>
                    )}
                    <button type="button" className="btn" onClick={() => this.formSubmit('save')}><span className="fa fa-save"></span> Save</button>
                    <button type="button" className="btn btn-green" onClick={() => this.formSubmit('lock')}><span className="fa fa-lock"></span> Save and Lock</button>
                </form>
            </div>
        );
    }
}