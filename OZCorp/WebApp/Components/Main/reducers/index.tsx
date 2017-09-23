import { combineReducers } from 'redux';
import JobOrder from './jobOrder';
import Notification from './notification';
import Report from './report';
import HomeSetting from './homesetting';
import ManageItem from './manageItem';
import ManageUser from './manageUser';
import Warehouse from './warehouse';
import PurchaseReport from './purchaseReport';
import CompanyPurchaseReport from './companyPurchaseReport';

const allreducers = combineReducers({
    jobOrder: JobOrder,
    notification: Notification,
    report: Report,
    homeSetting: HomeSetting,
    manageItem: ManageItem,
    manageUser: ManageUser,
    warehouse: Warehouse,
    purchaseReport: PurchaseReport,
    companyPurchaseReport: CompanyPurchaseReport
});

export default allreducers;