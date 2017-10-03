import * as Store from 'store';

const initialState = {
    blocks: Store.get('itemblock') ? Store.get('itemblock').blocks : [],
    blockTypes: [
        { id: 0, name: 'Big' },
        { id: 1, name: 'Medium' },
        { id: 2, name: 'Wide' },
        { id: 3, name: 'Tall' },
    ],
    editable: false,
}

export default function (state = initialState, action) {

    switch (action.type) {
        case 'ITEMBLOCK_LIST_SUCCESS':
            state = {
                ...state,
                blocks: [...action.payload.data.blocks],
                editable: action.payload.data.editable
            }
            break;
        case 'ITEMBLOCK_ADD_SUCCESS':
            state = {
                ...state,
                blocks: [{ ...action.payload.data },
                ...state.blocks
                ]
            }
            break;
    }
    Store.set('itemblock', state);
    return state;
}