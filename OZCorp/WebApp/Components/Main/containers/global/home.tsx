import * as React from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import { api, MethodType } from '../../../Common/api';
import { Box, BoxItem, BoxBig, BoxGroup } from '../box';

class Home extends React.Component<any, any>{
    componentDidMount() {
        api('ITEMBLOCK_LIST', MethodType.Get, '/itemblocks/list');
    }
    render() {
        const { list, blockTypes } = this.props.blocks;
        console.log(this.props);
        return (
            <div>
                <div className="row">
                    <h4>Select Blocks To Create!</h4>
                    {blockTypes.map(item =>
                        <a
                            key={item.id}
                            className="waves-effect waves-light btn"
                            onClick={() => api('ITEMBLOCK_ADD', MethodType.Get, `/itemblocks/CreateBlock?type=${item.id}`)}
                        >{item.name}</a>)}
                </div>
                <Box>
                    {list.map(items =>
                        <div key={items[0].groupId}>{(items.length <= 1
                            ? <BoxItem
                                Id={items[0].id}
                                BoxType={BoxBig}
                                ImageBackgroundUrl=""
                                Content=""
                                IsModified={false}
                                LogoUrl="" />
                            : <BoxItem
                                Id={items[0].id}
                                BoxType={BoxGroup}>
                                {items.map(item => (
                                    <BoxItem
                                        key={item.id}
                                        Id={item.id}
                                        BoxType={item.type}
                                    />
                                ))}
                            </BoxItem>
                        )}
                        </div>
                    )}
                </Box>
            </div>
        );
    }
}
export default withRouter(connect((state) => ({
    blocks: state.itemBlock
}))(Home));