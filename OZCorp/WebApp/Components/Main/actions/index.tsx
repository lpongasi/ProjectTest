import { bindActionCreators } from 'redux';

const Dispatch = (type: string = null, data: any = null) => {
    return {
        type: type,
        data: data
    }
}
const Dispatcher = (dispatch) =>
    (bindActionCreators({
        Dispatch
    }, dispatch));

export default Dispatcher;