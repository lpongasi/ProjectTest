
const initialState = {
    blocks: [],
    slideBlocks: [],
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
                ...action.payload.data,
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
        case 'ITEMSLIDEBLOCK_ADD_SUCCESS':
            state = {
                ...state,
                slideBlocks: [{ ...action.payload.data },
                ...state.slideBlocks
                ]
            }
            break;
    }

    return state;
}