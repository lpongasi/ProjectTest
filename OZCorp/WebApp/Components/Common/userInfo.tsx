import * as React from 'react';

export default class UserInfo extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
    }
    render() {
        return (
            <div className="card col s12 m6 l4">
                <div className="card-content">
                    <div className="right-align">
                        {this.props.user.purchaseDateString}
                    </div>
                    <div className="title">
                        Customer Info
                    </div>
                    Name: <b>{this.props.user.fullName}</b>
                    <br />
                    Email: <b>{this.props.user.email}</b>
                    <br />
                    Company Name: <b>{this.props.user.companyName}</b>
                    <br />
                    Company Addess: <b>{this.props.user.companyAddress}</b>
                    <br />
                    Company Contact: <b>{this.props.user.companyContact}</b>
                </div>
            </div>
        );
    }

}