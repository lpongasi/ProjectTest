import * as React from 'react';
import * as $ from 'jquery';
export class StoreCategory extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
        this.state = {
            category: [],
            subCategory: []
        };
        this.categoryChange = this.categoryChange.bind(this);
        this.categoriesChange = this.categoriesChange.bind(this);
    }
    categoriesChange() {
        this.props.onCategoryChange($(this.refs['categoryId']).val(), $(this.refs['subCategoryId']).val());
    }

    categoryChange(e: any) {
        var thisClass = this;
        $.post('/Common/SubCategories/' + e.target.value,
            data => {
                thisClass.setState({
                    subCategory: data.data
                },
                    thisClass.categoriesChange
                );
            });
    }
    componentDidMount() {
        var thisClass = this;
        $.post('/Common/Categories',
            categoryData => {
                $.post('/Common/SubCategories/' + 0,
                    subdata => {
                        thisClass.setState({
                            category: categoryData.data,
                            subCategory: subdata.data
                        });
                    },
                    'json');
            },
            'json'
        );
    }
    render() {
        return (
            <div className="row">
                <div className="col s12 m6 l6">
                    <label>Category</label>
                    <select className="browser-default" defaultValue="" ref="categoryId" onChange={(ev: any) => this.categoryChange(ev)}>
                        <option value="" >ALL</option>
                        {this.state.category.map((c: any, k: any) =>
                            <option key={'C' + k} value={c.key}>{c.value} </option>
                        )}
                    </select>
                </div>
                <div className="col s12 m6 l6">
                    <label>Sub Category</label>
                    <select className="browser-default" defaultValue="" ref="subCategoryId" onChange={() => this.categoriesChange()}>
                        <option value="">ALL</option>
                        {this.state.subCategory.map((c: any, k: any) =>
                            <option key={'SC' + k} value={c.key}>{c.value}</option>
                        )}
                    </select>
                </div>
            </div >
        );
    }
}