import * as $ from 'jquery';
import Store from './store';
import { StateLifeCycle } from './common';

export enum MethodType {
    Get = ('get') as any,
    Post = ('post') as any,
    Put = ('put') as any,
    Delete = ('delete') as any
}

export const dispatcher = (type: string, payload: any = null) =>
    Store.dispatch({
        type: type,
        payload: { ...payload }
    });

export const api = (
    dispatchPrefix: string = 'FETCH',
    methodType: MethodType = MethodType.Get,
    url: string = '',
    data: any = null,
    withFile: any = false,
    callBack: any = null
) => {
    dispatcher(`${dispatchPrefix}_${StateLifeCycle.Started}`);
    dispatcher(`LOADING_${StateLifeCycle.Started}`);
    $.ajax(!withFile
        ? {
            method: methodType.toString(),
            url: url,
            dataType: 'json',
            cache: false,
            data: data ? { ...data } : {}
        }
        : {
            url: url,
            type: 'POST',
            data: data,
            async: true,
            cache: false,
            contentType: false,
            processData: false
        }
    )
        .done((data) => {
            dispatcher(`${dispatchPrefix}_${StateLifeCycle.Success}`, { ...data });
            dispatcher(`LOADING_${StateLifeCycle.End}`);
        })
        .fail((jqXhr, textStatus, errorThrown) => {
            dispatcher(`${dispatchPrefix}_${StateLifeCycle.Error}`, { ...jqXhr.responseJSON, errorThrown, textStatus });
            dispatcher(`LOADING_${StateLifeCycle.Error}`, { ...jqXhr.responseJSON, errorThrown });
        })
        .always(() => {
            dispatcher(`${dispatchPrefix}_${StateLifeCycle.End}`);
            if (callBack) {
                callBack();
            }
        });
};