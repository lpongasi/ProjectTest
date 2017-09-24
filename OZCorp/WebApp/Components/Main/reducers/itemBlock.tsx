

const initialState = {
    list: require('store').get('itemblock') ? require('store').get('itemblock').list : [],
    blockTypes: [
        { id: 0, name: 'Big' },
        { id: 1, name: '4 Medium' },
        { id: 2, name: '2 Wide' },
        { id: 3, name: '2 Tall' },
        { id: 4, name: '1 Tall, 2 Medium' },
        { id: 5, name: '1 Wide, 2 Medium' },
        { id: 6, name: '2 Medium, 1 Wide ' },
    ]
}

export default function (state = initialState, action) {

    switch (action.type) {
        case 'ITEMBLOCK_LIST_SUCCESS':
            console.log(action);
            state = {
                ...state,
                list: [...action.payload.data]
            }
            break;
        case 'ITEMBLOCK_ADD_SUCCESS':
            console.log(action);
            state = {
                ...state,
                list: [[...action.payload.data],
                ...state.list
                ]
            }
            break;
    }
    require('store').set('itemblock', state);
    return state;
}