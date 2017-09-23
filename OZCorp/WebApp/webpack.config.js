const path = require('path');
const mode = require('./webpack/mode');
const plugins = require('./webpack/plugins');
const extractTextPlugin = require('extract-text-webpack-plugin');
const componentDir = path.resolve(__dirname, 'components');
const componentOutput = path.resolve(__dirname, 'wwwroot', 'assets');
const rootDir = path.resolve(__dirname, 'wwwroot');

module.exports = {
  devtool: mode.IS_PROD ? false : 'source-map',
  profile: true,
  stats: {
    assets: true,
    children: true,
    chunks: true,
    colors: true,
    errorDetails: true,
    errors: true,
    hash: true,
    modules: true,
    publicPath: true,
    reasons: true,
    source: true,
    timings: true,
    version: true,
    warnings: true
  },
  entry: {
    bundle: [
      path.resolve(rootDir, 'lib', 'jquery-ui', `jquery-ui${mode.IS_DEV?'':'.min'}.js`),
      path.resolve(rootDir, 'lib', 'jquery-validation', 'dist', `jquery.validate${mode.IS_DEV ? '' : '.min'}.js`),
      path.resolve(rootDir, 'lib', 'jquery-validation-unobtrusive', `jquery.validate.unobtrusive${mode.IS_DEV?'':'.min'}.js`),
      path.resolve(rootDir, 'lib', 'materialize', 'dist', 'js', `materialize${mode.IS_DEV ? '' : '.min'}.js`),
      path.resolve(componentDir, 'Main', 'main.tsx'),
      path.resolve(rootDir, 'js', 'site.js')
    ]
  },
  output: {
    filename: '[name].js',
    chunkFilename: '[id].js',
    path: componentOutput
  },
  resolve: {
    extensions: [
      '.ts', '.tsx',
      '.js', '.jsx',
      '.CSS', '.SCSS',
      '.otf', '.eot', '.svg', '.ttf', '.woff', '.woff2',
      '.png', '.jpg', '.jpeg', '.gif'
    ]
  },
  plugins,
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        exclude: /node_modules/,
        use: {
          loader: 'ts-loader',
          options: {
            transpileOnly: true
          }
        }
      },
      {
        test: /\.scss$/,
        exclude: /node_modules/,
        use: extractTextPlugin.extract({
          fallback: 'style-loader',
          use: [
            {
              loader: 'css-loader',
              options: {
                modules: false,
                importLoaders: 2
              }
            },
            {
              loader: 'postcss-loader',
              options: {
                plugins: (loader) => [
                  require('postcss-import')({ root: loader.resourcePath }),
                  require('postcss-url')(),
                  require('postcss-cssnext')({ browsers: ['last 2 version', 'ie >= 9', 'Android >= 4', 'ios >= 7'] }),
                  require('postcss-svg')(),
                  require('postcss-sprites')(),
                  require('postcss-browser-reporter')(),
                  require('postcss-reporter')()
                ]
              }
            },
            {
              loader: 'sass-loader',
              options: {
                modules: false,
                importLoaders: 3
              }
            }
          ]
        })
      },
      {
        test: /\.(otf|eot|ttf|svg|woff|woff2)$/,
        use: [{
          loader: 'file-loader',
          options: {
            modules: true,
            name: '../font/[name].[ext]'
          }
        }]
      },
      {
        test: /\.(png|jpg|jpeg|gif)$/,
        use: [{
          loader: 'file-loader',
          options: {
            modules: true,
            name: '../image/[name].[ext]'
          }
        }]
      },
      {
        test: require.resolve('jquery'),
        use: [{
          loader: 'expose-loader',
          options: '$'
        }, {
          loader: 'expose-loader',
          options: 'jQuery'
        }]
      }
    ]
  }
};