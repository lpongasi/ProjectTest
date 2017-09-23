import * as React from 'react';
import CommonTable from '../../common/commontable';
import Dispatcher from '../../actions';
import { manageUser } from '../../stateprops';
import { connect } from 'react-redux';
@connect(manageUser, Dispatcher)
export default class ItemTable extends React.Component<any, any> {
    _url_Create: string ='/Manage/User/register';
    _url_Read: string = '/Manage/User/list';
    _url_Update: string = '/Manage/User/edit';
    _url_Reset: string = '/Manage/User/ResetPassword';
    _url_Delete: string = '/Manage/User/delete';
    render() {
        return (
            <div className="content-padding-top">
                <h4><span className="fa fa-users"></span> Manage User</h4>
                <div className="row col s12">
                    <a className="btn" href={this._url_Create}><i className="fa fa-plus-square"> </i> Create New</a>
                </div>

                <CommonTable readUrl={this._url_Read} dispatcherId="MANAGEUSER_LIST" >
                    <table className="striped highlight responsive-table">
                        <thead>
                            <tr>
                                <th>COMPANY NAME</th>
                                <th>EMAIL</th>
                                <th>Name</th>
                                <th>COMPANY ADDRESS</th>
                                <th>COMPANY CONTACT</th>
                                <th>DISCOUNT</th>
                                <th>TAX</th>
                                <th width="150"></th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.props.manageUser.list.map((item: any, i: any) =>
                                <tr key={item.id}>
                                    <td>{item.companyName}</td>
                                    <td>{item.email}</td>
                                    <td>{item.fullName}</td>
                                    <td>{item.companyAddress}</td>
                                    <td>{item.companyContact}</td>
                                    <td>{item.discount}</td>
                                    <td>{item.tax}</td>
                                    <td>
                                        <a href={this._url_Update + '/' + item.id} className="btn btn-floating"> <span className="fa fa-edit"> </span>  </a>
                                        <a href={this._url_Reset + '/' + item.id} className="btn btn-green btn-floating"> <span className="fa fa-refresh"> </span>  </a>
                                         <a href={this._url_Delete + '/' + item.id} className="btn btn-red btn-floating "> <span className="fa fa-times"> </span>  </a>
                                    </td>
                                </tr>
                            )}
                        </tbody>
                    </table>
                </CommonTable>
            </div >
        );
    }
}