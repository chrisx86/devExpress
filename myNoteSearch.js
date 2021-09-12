search bar click
_setFocusedItem: function($target) {
    debugger;
    if (!$target || !$target.length) {
        return
    }
    this._updateFocusedItemState($target, true);
    this.onFocusedItemChanged(this.getFocusedItemId());
    if (this.option("selectOnFocus")) {
        this._selectFocusedItem($target)
    }
},
}
},

_focusInHandler: function(e) {
        debugger;
        var isValidationMessageShownOnFocus = "auto" === this.option("validationMessageMode");
        if (this._canValueBeChangedByClick() && isValidationMessageShownOnFocus) {
            var _this$_validationMess;
            var $validationMessageWrapper = null === (_this$_validationMess = this._validationMessage) || void 0 === _this$_validationMess ? void 0 : _this$_validationMess.$wrapper();
            null === $validationMessageWrapper || void 0 === $validationMessageWrapper ? void 0 : $validationMessageWrapper.removeClass("dx-invalid-message-auto");
            clearTimeout(this.showValidationMessageTimeout);
            this.showValidationMessageTimeout = setTimeout((function() {
                return null === $validationMessageWrapper || void 0 === $validationMessageWrapper ? void 0 : $validationMessageWrapper.addClass("dx-invalid-message-auto")
            }), 150)
        }
        return this.callBase(e)
    },


    當您選擇所有項目或選擇 / 取消選擇標題過濾器中的“ 全選” 項目時， DataGrid 組件會觸發 onOptionChanged 回調。 DataGrid 組件使用或取決於是否選擇或取消選擇所有項目來初始化回調的value參數： excludeinclude

onOptionChanged: function(e) {
    console.log(e.value);
}
onOptionChanged: function(e) {
    switch (e.value) {
        case "exclude":
            DB.SetMasterFilter(source.Name, DB.GetAvailableFilterValues(source.Name));
            break;
        case "include":
            DB.ClearMasterFilter(source.Name);
            break;
    }
}

// grid headerfilter is activated via extension
var gridOptions = grid.option();
if (gridOptions.headerFilter) {
    gridOptions.headerFilter.visible = filterColumns;
    gridOptions.headerFilter.allowSearch = true;
}
// grid rowfilter is activated via extension
if (rowFilter) {
    grid.option({
        filterRow: {
            visible: rowFilter
        }
    });
}
// Autfilter is activated via extension
// I create a new datastore with the copy from the gridviews dataobject.
// The paginate has to set off because the filter loads only a page else.
if (autoFilter) {
    var source = new DevExpress.data.DataSource({
        store: gridOptions.dataSource.store,
        paginate: false
    });
    // The source object gets additional properties for easy access later
    source.Name = itemName;
    source.Item = item;
    grid.option({
        // these event is raised whenever the visible rows are changed.
        // So i have to call the masterfilter function.
        onContentReady: function(e) {
            source.component = e.component;
            onGridSetmasterfilter(source);
        },
        // Also i have to watch if the SelectAll-Item is selected by any column.
        // In this case, the set masterfilter must also be called
        // because the onContentReady is not called, when the filter is not changed.
        onOptionChanged: function(e) {
            switch (e.value) {
                case "exclude":
                case "include":
                    onGridSetmasterfilter(source);
                    break;
            }
        }
    });

    function onGridSetmasterfilter(source) {
        // load all available items for the masterfilter.
        // this array differs from the grids datasource.
        // DB is the current Dashboard-Instance.
        var values = DB.GetAvailableFilterValues(source.Name);
        // get the current filter over all columns or rowfilter.
        var activeFilter = source.component.getCombinedFilter();
        if (activeFilter) {
            // now i can filter the stored datasource and get 
            // all rows because of turnedoff pagination.
            // After the load i can now get the masterfilter items
            // with the index property of the grids datasource which is the key.
            // So the masterfilter is set depending on grids visible rows.
            source.filter(activeFilter); // <= Code corrected
            source.load()
                .done(function(data) {
                    var newValues = Array.from(data, x => values[x.index]);
                    source.masterFilter = newValues;
                    DB.SetMasterFilter(source.Name, newValues);
                });
        } else {
            // if no filter is set, i have to check if any of the columns has exclude filter set.
            source.filterType = GetColumnFilterType(source.component);
            switch (source.filterType) {
                case "exclude": // set the masterfilter with all values
                    DB.SetMasterFilter(source.Name, values);
                    break;
                case "include": // clear the masterfilter
                    DB.ClearMasterFilter(source.Name);
                    break;
            }
        }
    }

    // check if any column hast exclude filter.
    function GetColumnFilterType(component) {
        var c = component.columnCount();
        for (i = 0; i < c; i++) {
            if (component.columnOption(i, "filterType") == "exclude") {
                return "exclude";
            }
        }
        return "include";
    }

    // if you have set to much masterfilters you can also clear easy all filters
    // without loosing the current drilldown state 
    // (found in another solution in the support center).
    function ClearAllFilter() {
        var state = JSON.parse(DB.GetDashboardState());
        $.each(state.Items, function(index, element) {
            element.MasterFilterValues = [];
        });
        var newState = JSON.stringify(state);
        DB.SetDashboardState(newState);
    }











    /*
    <div class="dx-list-select-all">
        <div class="dx-list-select-all-checkbox dx-show-invalid-badge dx-checkbox dx-widget" role="checkbox" aria-checked="false"><input type="hidden" value="false">
            <div class="dx-checkbox-container"><span class="dx-checkbox-icon"></span></div>
        </div>
        <div class="dx-list-select-all-label">Select All</div>
    </div>
    
    <input autocomplete="off" aria-label="Search" class="dx-texteditor-input" type="text" spellcheck="false" tabindex="0" role="textbox">
    
    
    <div class="dx-list-search dx-show-invalid-badge dx-textbox dx-texteditor dx-editor-outlined dx-searchbox dx-show-clear-button dx-texteditor-empty dx-widget">
        <div class="dx-texteditor-container">
            <div class="dx-texteditor-input-container">
                <div class="dx-icon dx-icon-search"></div><input autocomplete="off" aria-label="Search" class="dx-texteditor-input" type="text" spellcheck="false" tabindex="0" role="textbox">
                <div data-dx_placeholder="Search" class="dx-placeholder"></div>
            </div>
            <div class="dx-texteditor-buttons-container"><span class="dx-clear-button-area"><span class="dx-icon dx-icon-clear"></span></span>
            </div>
        </div>
    </div>
    
    */