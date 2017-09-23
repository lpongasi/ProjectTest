import * as React from 'react';
import PurchaseOrderDetail from '../../../orderDetail';
const purchaseOrderDetail = ({ match }) => (
    <div>
        <PurchaseOrderDetail poId={match.params.id} />
    </div>
);

export default purchaseOrderDetail;