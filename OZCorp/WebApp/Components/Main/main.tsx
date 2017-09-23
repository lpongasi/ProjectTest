import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { createStore, applyMiddleware } from 'redux';
import thunk from 'redux-thunk';
import { Provider } from 'react-redux';
import Logger from './component/logger';
import Mode from '../../webpack/mode';
import '../../wwwroot/lib/fontawesome/scss/font-awesome.scss';
import '../../wwwroot/lib/materialize/sass/materialize.scss';
import '../../wwwroot/scss/bundle.scss';
import Layout from './pages';
import allReducers from './reducers';
import '../HistoryTable';
import '../categoryManagement';

let middleware = [thunk];
if (Mode.IS_DEV) {
    middleware = [...middleware, Logger];
}
const store = createStore(allReducers, applyMiddleware(...middleware));
ReactDOM.render(
    <Provider store={store}>
        <Layout />
    </Provider>
    , document.getElementById('mainApp'));