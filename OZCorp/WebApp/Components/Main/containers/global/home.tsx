import * as React from 'react';
import { connect } from 'react-redux';
import { withRouter } from 'react-router-dom';
import Slider from 'react-slick';
import { api, MethodType } from '../../../Common/api';
import { Box, BoxItem, BoxBig, BoxGroup } from '../box';

class Home extends React.Component<any, any>{
    constructor(props) {
        super(props);
        this.state = {
            modify: false
        };
    }
    componentDidMount() {
        api('ITEMBLOCK_LIST', MethodType.Get, '/itemblocks/list');
    }
    render() {
        const { blocks, blockTypes, editable, slideBlocks } = this.props.blocks;
        const isEditable = editable && this.state.modify;
        const sliderSettings = {
            dots: false,
            infinite: true,
            speed: 500,
            slidesToShow: 1,
            slidesToScroll: 1,
            adaptiveHeight: true,
            autoplay: true,
            autoplaySpeed: 5000,
        };
        return (
            <div>
                {(editable && <a
                    className={`btn ${this.state.modify ? 'btn-green' : ''}`}
                    onClick={() => this.setState({ modify: !this.state.modify })}>
                    {(this.state.modify
                        ? <span><span className="fa fa-edit"> </span> Show Client Mode</span>
                        : <span><span className="fa fa-edit"> </span> Edit Page</span>)}
                </a>)}
                {(isEditable && <div className="edit-slide">
                    <h3>Slides Show Items</h3>
                    <h6>Note: slide show doesn't display logo </h6>
                    <a
                        className="waves-effect waves-light btn"
                        onClick={() => api('ITEMSLIDEBLOCK_ADD', MethodType.Get, '/itemblocks/CreateSlideItem')}
                    ><span className="fa fa-plus"> </span> Add New Item</a>
                    <Box>
                        {slideBlocks && slideBlocks.map(item => (<BoxItem
                            key={item.id}
                            GroupId={item.groupId}
                            Id={item.id}
                            BoxType={item.type}
                            ImageBackgroundUrl={item.backgroundUrl}
                            LogoUrl={item.logoUrl}
                            Editable={isEditable}
                        />))}
                    </Box>
                </div>)}
                {
                    (!isEditable && slideBlocks && slideBlocks.length > 0 && <div className="row">
                        <Slider {...sliderSettings}>
                            {slideBlocks.map(item => (<div className="app-slider" key={`Slide-${item.id}`}>
                                <img src={item.backgroundUrl ? item.backgroundUrl : '/images/no-image-Orig.png'} />
                            </div>))}
                        </Slider>
                    </div>)
                }
                {(isEditable &&
                    <div className="row">
                        <h4>Select Blocks To Create!</h4>
                        {blockTypes.map(item =>
                            <a
                                key={item.id}
                                className="waves-effect waves-light btn"
                                onClick={() => api('ITEMBLOCK_ADD', MethodType.Get, `/itemblocks/CreateBlock?type=${item.id}`)}
                            >{item.name}</a>)}
                    </div>
                )}
                <Box>
                    {blocks && blocks.map(item => (<BoxItem
                        key={item.id}
                        GroupId={item.groupId}
                        Id={item.id}
                        BoxType={item.type}
                        ImageBackgroundUrl={item.backgroundUrl}
                        LogoUrl={item.logoUrl}
                        Editable={isEditable}
                    />))}
                </Box>
            </div>
        );
    }
}
export default withRouter(connect((state) => ({
    blocks: state.itemBlock
}))(Home));
