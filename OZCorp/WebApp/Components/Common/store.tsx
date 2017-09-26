import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
//import logger from './logger';
import allReducers from '../Main/reducers';
//import { isProd } from '../../webpack/global';

let middleware: any = [thunk];
//if (!isProd) {
//    middleware = [...middleware, logger]
//}
export default createStore(allReducers, applyMiddleware(...middleware));