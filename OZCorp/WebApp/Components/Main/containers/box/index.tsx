import * as React from 'react';

const Style = {
    createImage(imageUrl: string): object {
        return {
            backgroundImage: 'url(' + imageUrl + ')',
            backgroundRepeat: 'no-repeat',
            WebkitBackgroundSize: 'cover',
            MozBackgroundSize: 'cover',
            OBackgroundSize: 'cover',
            BackgroundSize: 'cover'
        };
    }
}

//Tall
//320 x 640
//WIDE
//650 X 315
//MEDIUM
//320 X 315

//CENTER LOGO 
//100 X 100

export const BoxTall = 'tall';
export const BoxBig = 'big';
export const BoxWide = 'wide';
export const BoxMedium = 'medium';
export const BoxGroup = 'big-group';

export class BoxProps {
    public constructor(boxProps: BoxProps) {
        this.BoxType = boxProps.BoxType ? boxProps.BoxType : BoxMedium;
        this.ImageBackgroundUrl = boxProps.ImageBackgroundUrl && this.BoxType != BoxGroup ? boxProps.ImageBackgroundUrl : this.BoxType != BoxGroup ? '/images/no-image-Orig.png' : null;
        this.LogoUrl = boxProps.LogoUrl;
        this.Content = boxProps.Content ? boxProps.Content : 'Content Not Available!';
        this.IsModified = true;
    }
    public Id: string;
    public BoxType?: string;
    public ImageBackgroundUrl?: string;
    public LogoUrl?: string;
    public Content?: string;
    public IsModified?: boolean;
}
export class Box extends React.Component<any, any>{
    render() {
        return (<ul className="box box-sortable">{this.props.children}</ul>);
    }
}
export class BoxItem extends React.Component<BoxProps, BoxProps>{
    constructor(props: BoxProps) {
        super(props);
        this.state = new BoxProps(this.props);

    }
    backgroundChange(input: React.ChangeEvent<HTMLInputElement>) {
        let reader = new FileReader();
        let file = input.target.files[0];

        reader.onloadend = () => {
            this.setState({
                ImageBackgroundUrl: reader.result,
                IsModified: true
            });
        }

        reader.readAsDataURL(file);
    }
    logoChange(input: React.ChangeEvent<HTMLInputElement>) {
        let reader = new FileReader();
        let file = input.target.files[0];

        reader.onloadend = () => {
            this.setState({
                LogoUrl: reader.result,
                IsModified: true
            });
        }

        reader.readAsDataURL(file);
    }
    render() {
        return (
            <li className={'box-item ' + this.state.BoxType + ' editable'}>
                <div className="content-wrapper" style={Style.createImage(this.state.ImageBackgroundUrl)}>
                    {this.props.BoxType != BoxGroup
                        ? <div className="content">
                            {this.state.LogoUrl ? <img src={this.state.LogoUrl} className="logo" /> : null}
                            <div className="box-inputs">
                                <div className="file-field input-field logo-input">
                                    <div className="btn">
                                        <span><span className="fa fa-file-image-o"></span> Logo</span>
                                        <input type="file" onChange={this.logoChange.bind(this)} />
                                    </div>
                                </div>
                                <div className="file-field input-field back-input">
                                    <div className="btn">
                                        <span><span className="fa fa-picture-o"></span> Background</span>
                                        <input type="file" onChange={this.backgroundChange.bind(this)} />
                                    </div>
                                </div>
                                {(this.state.IsModified && <a className="waves-effect waves-light btn btn-save btn-green"><span className="fa fa-save"> </span> Save Changes</a>)}
                            </div>
                        </div>
                        : <Box> {this.props.children}</Box>}
                </div>
            </li>);
    };
}