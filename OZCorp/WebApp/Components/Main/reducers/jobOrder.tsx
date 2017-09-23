const initialState = {
    list: [],
    joDetail:null
}

export default function (state = initialState, action) {
    switch (action.type) {
        case 'JO_FILTER':
            state = {
                ...state,
                list: action.data
            }
            break;
        case 'JO_DETAIL':
            state = {
                ...state,
                joDetail: action.data
            }
            break;
    }
    return state;
}