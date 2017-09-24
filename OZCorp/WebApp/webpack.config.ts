import * as webpack from 'webpack';
import { isProd } from './webpack/global';
import { devServer, entry, output, stats, resolve, plugins, webpackModule } from './webpack/config';

const config: webpack.Configuration = {
    devtool: isProd ? false : 'source-map',
    devServer,
    stats,
    resolve,
    module: webpackModule,
    entry,
    output,
    plugins,
};

export default config;