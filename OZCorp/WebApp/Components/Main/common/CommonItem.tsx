import * as React from 'react';
interface ICommonItem {
    title: string;
    subTitle: any;
    description: any;
    imageUrl: string;
    actionLinks: any;
}
export default class CommonItem extends React.Component<ICommonItem, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            hoverImage: null,
            imageUrl: this.props.imageUrl
        };
        this.imageLarge = this.imageLarge.bind(this);
        this.imageLargeClose = this.imageLargeClose.bind(this);

    }
    imageLarge(e: React.MouseEvent<HTMLImageElement>) {
        $(this.refs['modal-image']).attr('src', this.state.imageUrl.replace('.', '-Orig.'));
        $(this.refs['modal']).css('display', 'block');
    }
    imageLargeClose() {
        $(this.refs['modal']).css('display', 'none');
    }
    render() {
        return (
            <div className="col s12 m4 l3">
                <div className="modal-image" ref="modal" onClick={this.imageLargeClose}>
                    <img className="modal-content" ref="modal-image" />
                </div>
                <div className="card sticky-action">
                    <div className="card-image">
                        <img src={this.state.imageUrl} onClick={this.imageLarge} alt="" />
                    </div>
                    <div className="card-content">
                        <span className="card-title activator grey-text text-darken-4 truncate">{this.props.title}</span>
                        <p>{this.props.subTitle}</p>
                    </div>
                    <div className="card-reveal">
                        <span className="card-title grey-text text-darken-4">{this.props.title}<i className="fa fa-times right"></i></span>
                        <p>{this.props.description}</p>
                    </div>
                    {this.props.actionLinks != null
                        ? <div className="card-action">
                            {this.props.actionLinks}
                        </div> : <span></span>}
                </div>
            </div>
        );
    }
}
