import * as path from 'path';

declare var __dirname;

export const isProd = process.env['NODE_ENV'] === 'production';

export const root = path.resolve(__dirname, '..');

export const assetPath = path.resolve(root, 'wwwroot');

export const outputPath = path.resolve(assetPath, 'assets');

export const scssInputPath = path.resolve(assetPath, 'scss');

export const nodePath = path.resolve(root, 'node_modules');

export const clientAppPath = path.resolve(root, 'ClientApp');