import * as React from 'react';
import { api, MethodType } from '../../../Common/api';
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
        this.IsModified = false;
        this.SavingChanges = false;
    }
    public Id: string;
    public GroupId: string;
    public BoxType?: string;
    public ImageBackgroundUrl?: string;
    public LogoUrl?: string;
    public Content?: string;
    public IsModified?: boolean;
    public SavingChanges?: boolean;
    public Editable: boolean;
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
    submit(e: React.SyntheticEvent<HTMLFormElement>) {
        e.preventDefault();
        var formData = new FormData(e.currentTarget);
        this.setState({
            SavingChanges: true
        });
        api('ITEMBLOCK_LIST', MethodType.Post, '/itemblocks/Update', formData, true, () => this.setState({
            IsModified: false,
            SavingChanges: false
        }));
    }
    removeBlock(id) {
        api('ITEMBLOCK_LIST', MethodType.Post, '/itemblocks/Remove', { id });
    }
    render() {
        return (
            <li className={'box-item ' + this.state.BoxType}>               
                <div className="content-wrapper" style={Style.createImage(this.state.ImageBackgroundUrl)}>
                    {(this.props.Editable &&
                        <div className="remove-block">
                            <a className="btn btn-red" onClick={() => this.removeBlock(this.props.Id)}><span className="fa fa-times"> </span> Remove Block</a>
                        </div>)}
                    {this.props.BoxType != BoxGroup
                        ? <div className="content">
                            {this.state.LogoUrl ? <img src={this.state.LogoUrl} className="logo" /> : null}
                            {(this.props.Editable &&
                                <div>
                                    <form onSubmit={this.submit.bind(this)} encType="multipart/form-data" >
                                        <div className="box-inputs">
                                            <input type="hidden" name="id" value={this.props.Id} />
                                            <div className="row">
                                                <div className="file-field input-field col">
                                                    <div className="btn">
                                                        <span><span className="fa fa-file-image-o"></span> Logo</span>
                                                        <input type="file" name="logo" onChange={this.logoChange.bind(this)} />
                                                    </div>
                                                </div>
                                                <div className="file-field input-field col">
                                                    <div className="btn">
                                                        <span><span className="fa fa-picture-o"></span> Background</span>
                                                        <input type="file" name="background" onChange={this.backgroundChange.bind(this)} />
                                                    </div>
                                                </div>
                                            </div>
                                            {(this.state.SavingChanges && <h6>Saving changes. please wait...</h6>)}
                                            {(this.state.IsModified && !this.state.SavingChanges && <button className="waves-effect waves-light btn btn-save btn-green" type="submit" ><span className="fa fa-save"> </span> Save Changes</button>)}
                                        </div>
                                    </form>
                                </div>)}
                        </div>
                        : <Box>
                            {(this.props.Editable &&
                                <div className="remove-block">
                                    <a className="btn btn-red" onClick={() => this.removeBlock(this.props.GroupId)}><span className="fa fa-times"> </span> Remove Block</a>
                                </div>)}
                            {this.props.children}
                        </Box>}
                </div>
            </li>);
    };
}