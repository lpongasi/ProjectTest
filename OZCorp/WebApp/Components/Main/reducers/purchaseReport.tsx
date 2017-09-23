
const initialState = {
    itemReportViews: [],
    itemReportViewTotal: null
}
export default function (state = initialState, action) {
    switch (action.type) {
        case 'PURCHASE_REPORT':
            if (action.data == null) {
                state = {
                    ...state
                }
            } else {
                state = {
                    ...state,
                    itemReportViews: action.data.itemReportViews,
                    itemReportViewTotal: action.data.itemReportViewTotal
                }
            }
          
            break;
    }
    return state;
}