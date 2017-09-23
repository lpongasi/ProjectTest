const mode = require("./mode");
const webpack = require("webpack");
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const UglifyJSPlugin = require('uglifyjs-webpack-plugin');

module.exports = [];
module.exports.push(new webpack.DefinePlugin({
  "process.env": {
    "NODE_ENV": JSON.stringify(process.env.NODE_ENV)
  }
}));

module.exports.push(new ExtractTextPlugin({
  filename: "[name].css",
  allChunks: true
}));
//use use to separate other libraries
//module.exports.push(new webpack.optimize.CommonsChunkPlugin({
//  name: "commons",
//  filename: "commonBundle.js"
//}));

if (mode.IS_PROD) {
  //module.exports.push(new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/));
  module.exports.push(new webpack.LoaderOptionsPlugin({
    minimize: true,
    debug: false
  }),
    new webpack.optimize.UglifyJsPlugin({
      compress: {
        warnings: false,
        screw_ie8: true,
        conditionals: true,
        unused: true,
        comparisons: true,
        sequences: true,
        dead_code: true,
        evaluate: true,
        if_return: true,
        join_vars: true
      },
      output: {
        comments: false
      },
    }));
  //module.exports.push(new UglifyJSPlugin({
  //  compress: true,
  //  beautify: false,
  //  sourceMap: false,
  //  comments: false
  //}));
}