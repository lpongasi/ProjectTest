import { createLogger } from 'redux-logger';
import { StateLifeCycle } from './common';

const actionTransformer = action => {

  if (action && action.type === 'BATCHING_REDUCER.BATCH') {
    action.payload.type = action.payload.map(next => next.type).join(' => ');
    return action.payload;
  }

  return action;
};

const level = 'info';
const logger = {};
const mute = [
  `LOADING_${StateLifeCycle.Started}`,
  `LOADING_${StateLifeCycle.Error}`,
  `LOADING_${StateLifeCycle.End}`
];

for (const method in console) {
  if (console.hasOwnProperty(method)) {
    if (typeof console[method] === 'function') {
      logger[method] = console[method].bind(console);
    }
  }
}

logger[level] = function(...args) {
  const lastArg = args.pop();

  if (Array.isArray(lastArg)) {
    return lastArg.forEach(item => {
      console[level].apply(console, [...args, item]);
    });
  }

  console[level].apply(console, arguments);
};

export default createLogger({
  level,
  predicate: (getState, action) => mute.indexOf(action.type) < 0,
  actionTransformer,
  logger
});