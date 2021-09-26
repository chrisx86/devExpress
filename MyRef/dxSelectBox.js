(() => {
    let DataSrc = [
        "HD Video Player SuperPlasma 65 dxSelectBox",
        "SuperHD Video Player",
        "SuperPlasma 50",
        "SuperLED 50",
        "SuperLED 42",
        "SuperLCD 55",
        "SuperLCD 42",
        "SuperPlasma 65",
        "SuperLCD 70",
        "Projector Plus",
        "Projector PlusHT",
        "ExcelRemote IR",
        "ExcelRemote BT",
        "ExcelRemote IP"
    ];

    let DataSrc2 = [
        "ExcelRemote",
        "ExcelRemote",
        "ExcelRemote"
    ];
    let ds = new DevExpress.data.DataSource({
        store: DataSrc,
        load: function(LoadOptions){
            if(LoadOptions.searchValue != null){
                return DataSrc;
            }else{
                return DataSrc;
            }
        }
    });

    let customDataSource = new DevExpress.data.CustomStore({
        load: (loadOptions) => {
            return DataSrc;
        },
        byKey: (key) => {
            var d = new $.Deferred();
            return d.promise();
        }
        // Needed to process selected value(s) in the SelectBox, Lookup, Autocomplete, and DropDownBox
        // byKey: function(key) {
        //     var d = new $.Deferred();
        //     $.get('https://mydomain.com/MyDataService?id=' + key)
        //         .done(function(result) {
        //             d.resolve(result);
        //         });
        //     return d.promise();
        // }
    });


    inputGroupingName = $('#SelectBox').dxSelectBox({
        dataSource: customDataSource,
        searchEnabled: true,
        acceptCustomValue: true,
        wrapItemText: true,
        onValueChanged: function (e) {
            console.log(e);
        }
        
    }).dxSelectBox("instance");


    var source = new DevExpress.data.DataSource({
        store: DataSrc,
        paginate: true,
        pageSize: 10
    });
    //inputGroupingName.option('dataSource', null);
    //let groupingName = inputGroupingName.option('value') == null ? '' : inputGroupingName.option('value').trim();
    //nputGroupingName.option('dataSource', source);



})();

/*
new SelectBox(container, {
acceptCustomValue:false,
accessKey:undefined,
activeStateEnabled:true,
buttons: [],
dataSource:Edit,
deferRendering:true,
disabled:false,
displayExpr:undefined,
dropDownButtonComponent:null,
dropDownButtonRender:null,
dropDownButtonTemplate:"dropDownButton",
dropDownOptions:{},
elementAttr:{},
fieldComponent:null,
fieldRender:null,
fieldTemplate:null,
focusStateEnabled:true,
groupComponent:null,
grouped:false,
groupRender:null,
groupTemplate:"group",
height:undefined,
hint:undefined,
hoverStateEnabled:true,
inputAttr:{},
isValid:true,
itemComponent:null,
itemRender:null,
items:Edit,
itemTemplate:"item",
maxLength:null,
minSearchLength:0,
name:"",
noDataText:"No data to display",
onChange:null,
onClosed:null,
onContentReady:null,
onCopy:null,
onCustomItemCreating:function(e) { if(!e.customItem) { e.customItem = e.text; } },
onCut:null,
onDisposing:null,
onEnterKey:null,
onFocusIn:null,
onFocusOut:null,
onInitialized:null,
onInput:null,
onItemClick:null,
onKeyDown:null,
onKeyUp:null,
onOpened:null,
onOptionChanged:null,
onPaste:null,
onSelectionChanged:null,
onValueChanged:null,
opened:false,
openOnFieldClick:true,
placeholder:"Select",
readOnly:false,
rtlEnabled:false,
searchEnabled:false,
searchExpr:null,
searchMode:"contains",
searchTimeout:500,
showClearButton:false,
showDataBeforeSearch:false,
showDropDownButton:true,
showSelectionControls:false,
spellcheck:false,
stylingMode:"outlined",
tabIndex:0,
useItemTextAsTitle:false,
validationError:null,
validationErrors:null,
validationMessageMode:"auto",
validationStatus:"valid",
value:null,
valueChangeEvent:"change",
valueExpr:this,
visible:true,
width:undefined,
wrapItemText:false
});
 */


// $('#comboboxEQPTYPE').multiselect({
//     enableFiltering: true,
//     enableCaseInsensitiveFiltering: true,
//     filterBehavior: 'value',
//     maxHeight: 200,
//     onChange: function(element, checked) {
//         if (gotoPage != null && gotoPage == 'Grouping NPW Parameter') {
//             loadingMask.show();
//         }

//         let eqptype = $('#comboboxEQPTYPE').val();
//         $.when(loadEQPID(eqptype)).done(function(x2) {
//             let eqpids = $('#comboboxEQPID').val();
//             $.when(loadChamber(eqptype, eqpids)).done(function(x3) {
//                 let chambers = $('#comboboxChamber').val();
//                 $.when(loadNPWParameter(eqptype, eqpids, chambers)).done(function(x4) {
//                     loadingMask.hide();
//                 });
//             });
//         });
//     },
//     onDropdownShown: function(event) {
//         $(event.target).find('input[type="search"]').focus();
//         if (event.target.className.indexOf('open') > -1) {
//             event.target.className = event.target.className.replace('open', '');
//         }
//     }
// }); }
// });