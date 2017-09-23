
const initialState = {
    companyReportViews: [],
    companyReportViewTotal: null
}
export default function (state = initialState, action) {
    switch (action.type) {
        case 'COMPANYPURCHASE_REPORT':
            if (action.data == null) {
                state = {
                    ...state
                }
            } else {
                state = {
                    ...state,
                    companyReportViews: action.data.companyReportViews,
                    companyReportViewTotal: action.data.companyReportViewTotal
                }
            }
          
            break;
    }
    return state;
}