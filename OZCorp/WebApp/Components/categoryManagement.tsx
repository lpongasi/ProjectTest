import * as React from 'react';
import * as ReactDOM from 'react-dom';
import * as $ from 'jquery';
const elementId = 'categoryManagement';
export default class ItemTable extends React.Component<any, any> {
  constructor(props: any) {
    super(props);
    this.state = {
      unit: [],
      selectedUnit: $('#categoryManagement').attr('selectedUnit'),
      size: [],
      selectedSize: $('#categoryManagement').attr('selectedSize'),
      category: [],
      selectedCategory: $('#categoryManagement').attr('selectedCategory'),
      subCategory: [],
      selectedSubCategory: $('#categoryManagement').attr('selectedSubCategory')
    };
    this.addCategory = this.addCategory.bind(this);
    this.addSubCategory = this.addSubCategory.bind(this);
    this.categoryChange = this.categoryChange.bind(this);
    this.addUnit = this.addUnit.bind(this);
    this.addSize = this.addSize.bind(this);
  }
  addUnit() {
    var thisClass = this;
    $.post('/Manage/Item/UnitAdd',
      {
        name: $('#newUnitInput').val()
      }, data => {
        thisClass.setState({
          unit: data.data
        });
        Materialize.toast($('#newUnitInput').val() + ' Added!', 3000, 'rounded');
        $('#newUnitInput').val('');
      },
      'json'
    ).fail(() => {

    });
  }
  addSize() {
    var thisClass = this;
    $.post('/Manage/Item/SizeAdd',
      {
        name: $('#newSizeInput').val()
      }, data => {
        thisClass.setState({
          size: data.data
        });
        Materialize.toast($('#newSizeInput').val() + ' Added!', 3000, 'rounded');
        $('#newSizeInput').val('');
      },
      'json'
    ).fail(() => {

    });
  }
  addCategory() {
    var thisClass = this;
    $.post('/Manage/Item/CategoryAdd',
      {
        name: $('#newCategoryInput').val()
      }, data => {
        thisClass.setState({
          category: data.data
        });
        Materialize.toast($('#newCategoryInput').val() + ' Added!', 3000, 'rounded');
        $('#newCategoryInput').val('');
      },
      'json'
    ).fail(() => {

    });
  }
  addSubCategory() {
    var thisClass = this;
    $.post('/Manage/Item/SubCategoryAdd/' + $('#CategoryId').val(),
      {
        name: $('#newSubCategoryInput').val()
      }, data => {
        thisClass.setState({
          subCategory: data.data
        });
        Materialize.toast($('#newSubCategoryInput').val() + ' Added!', 3000, 'rounded');
        $('#newSubCategoryInput').val('');
      },
      'json'
    ).fail(() => {

    });
  }
  categoryChange(e: any) {
    var thisClass = this;
    $.post('/Manage/Item/SubCategories/' + e.target.value,
      data => {
        thisClass.setState({
          subCategory: data.data
        });
      });
  }
  componentDidMount() {
    var thisClass = this;

    $.post('/Manage/Item/Size',
      sizeData => {
        $.post('/Manage/Item/Unit',
          unitData => {
            $.post('/Manage/Item/Categories',
              categoryData => {
                $.post('/Manage/Item/SubCategories/' + this.state.selectedCategory,
                  subdata => {
                    thisClass.setState({
                      size: sizeData.data,
                      unit: unitData.data,
                      category: categoryData.data,
                      subCategory: subdata.data
                    });
                  },
                  'json');
              },
              'json'
            );
          },
          'json');
      },
      'json');
  }
  render() {
    return (
      <div className="content-padding-top">
        <div className="col s12 m6 l6">
          <label>Size</label>
          <select className="browser-default" id="sizeId" name="sizeId" defaultValue={this.state.selectedSize}>
            <option value="">Choose your option</option>
            {this.state.size.map((u: any, uk: any) =>
                <option key={'U' + uk} value={u.key}>{u.value}</option>
            )}
          </select>
        </div>
        <div className="input-field col s12 m6 l6">
          <input type="text" id="newSizeInput" name="newSizeInput" />
          <label htmlFor="newSizeInput">Add new Size</label>
          <a className="btn" onClick={() => this.addSize()}><span className="fa fa-plus"></span> Add</a>
        </div>
        <div className="col s12 m6 l6">
          <label>Unit of Measure</label>
          <select className="browser-default" id="UomId" name="UomId" defaultValue={this.state.selectedUnit}>
            <option value="0">Choose your option</option>
            {this.state.unit.map((u: any, uk: any) =>
                <option key={'U' + uk} value={u.key}>{u.value}</option>
            )}
          </select>
        </div>
        <div className="input-field col s12 m6 l6">
          <input type="text" id="newUnitInput" name="newUnitInput" />
          <label htmlFor="newUnitInput">Add new Unit of Measure</label>
          <a className="btn" onClick={() => this.addUnit()}><span className="fa fa-plus"></span> Add</a>
        </div>
        <div className="col s12 m6 l6">
          <label>Category</label>
          <select className="browser-default" id="CategoryId" name="CategoryId" defaultValue={this.state.selectedCategory} onChange={(ev: any) => this.categoryChange(ev)}>
            <option value="0" >Choose your option</option>
            {this.state.category.map((c: any, k: any) =>
                <option key={'C' + k} value={c.key}>{c.value} </option>
            )}
          </select>
        </div>
        <div className="input-field col s12 m6 l6">
          <input type="text" id="newCategoryInput" name="newCategoryInput" />
          <label htmlFor="newCategoryInput">Add new Category</label>
          <a className="btn" onClick={() => this.addCategory()}><span className="fa fa-plus"></span> Add</a>
        </div>

        <div className="col s12 m6 l6">
          <label>Sub Category</label>
          <select className="browser-default" id="SubCategoryId" name="SubCategoryId" defaultValue={this.state.selectedSubCategory}>
            <option value="0">Choose your option</option>
            {this.state.subCategory.map((c: any, k: any) =>
                <option key={'SC' + k} value={c.key}>{c.value}</option>
            )}
          </select>
        </div>
        <div className="input-field col s12 m6 l6">
          <input type="text" id="newSubCategoryInput" name="newSubCategoryInput" />
          <label htmlFor="newSubCategoryInput">Add new Sub Category</label>
          <a className="btn" onClick={() => this.addSubCategory()}><span className="fa fa-plus"></span> Add</a>
        </div>
      </div >
    );
  }
}

const element = document.getElementById(elementId);
if (element) {
    ReactDOM.render(
        <ItemTable/>,
        document.getElementById(elementId)
    );
}
