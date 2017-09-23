const initialState = {
    list: []
}
export default function (state = initialState, action) {
    switch (action.type) {
    case 'NOTIFY_FILTER':
        state = {
            ...state,
            list: action.data
        }
        break;
    }
    return state;
}