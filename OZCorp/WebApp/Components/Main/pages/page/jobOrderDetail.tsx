import * as React from 'react';
import JobOrderDetail from '../../containers/jobOrder/detail';
const jobOrderDetail = ({ match }) => (
    <div>
        <JobOrderDetail jobId={match.params.id} />
    </div>
);

export default jobOrderDetail;