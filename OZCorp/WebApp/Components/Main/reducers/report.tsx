const initialState = {
    list: [],
    repList: [],
    detail:null
}
export default function (state = initialState, action) {
    switch (action.type) {
    case 'REPORT_LIST':
        state = {
            ...state,
            list: action.data
        }
        break;
    case 'REPORT_REPLIST':
        state = {
            ...state,
            repList: action.data
        }
        break;
    case 'REPORT_DETAIL':
        state = {
            ...state,
            detail: action.data
        }
        break;
    }
    return state;
}