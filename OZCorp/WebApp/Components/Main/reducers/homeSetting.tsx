const initialState = {
    list: [],
    listAds: [],
    update: null,
    remove: null,
    edit: false
}

export default function (state = initialState, action) {
    switch (action.type) {
    case 'HOMESETTING_LIST':
        state = {
            ...state,
            list: action.data
        }
        break;
    case 'HOMESETTING_ADS':
        state = {
            ...state,
            listAds: action.data
        }
        break;
    case 'HOMESETTING_UPDATE':
        state = {
            ...state,
            update: action.data
        }
        break;
    case 'HOMESETTING_REMOVE':
        state = {
            ...state,
            remove: action.data
        }
        break;
    case 'HOMESETTING_CANEDIT':
        state = {
            ...state,
            edit: action.data
        }
        break;
    }
    return state;
}