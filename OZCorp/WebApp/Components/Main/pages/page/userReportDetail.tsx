import * as React from 'react';
import UserReportDetail from '../../containers/report/userReportDetail';
const userReportDetail = ({ match }) => (
    <div>
        <UserReportDetail repId={match.params.id} />
    </div>
);

export default userReportDetail;