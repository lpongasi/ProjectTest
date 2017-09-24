import * as React from 'react';
import * as ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import '../../wwwroot/lib/fontawesome/scss/font-awesome.scss';
import '../../wwwroot/lib/materialize/sass/materialize.scss';
import '../../wwwroot/scss/bundle.scss';
import Layout from './pages';
import '../HistoryTable';
import '../categoryManagement';
import store from '../Common/store';

ReactDOM.render(
    <Provider store={store}>
        <Layout />
    </Provider>
    , document.getElementById('mainApp'));