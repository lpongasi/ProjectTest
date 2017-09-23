const initialState = {
    list: []
}

export default function (state = initialState, action) {
    switch (action.type) {
    case 'WAREHOUSE_LIST':
        state = {
            ...state,
            list: action.data
        }
        break;
    }
    return state;
}