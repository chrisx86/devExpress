;
(() => {

    let columns = [{
            dataField: 'RowNumber',
            caption: 'RowNumber',
            width: 60,
            minWidth: 60,
            alignment: 'right',
            format: '',
            dataType: 'string',
            allowEditing: false,
            fixed: true,
            calculateFilterExpression: function (value, operation, target) {
                var column = this;
                if (value) {
                    if (operation == "contains") {
                        return [ rowObj => {
                            return new RegExp(value.replace("*", ".*")).test(column.calculateCellValue(rowObj))
                        }, '=', true];
                    }
                    else {
                        return [column.dataField, operation, value];
                    }
                }
            }
        },
        {
            dataField: 'EQPID',
            caption: 'EQPID',
            width: 120,
            minWidth: 120,
            alignment: 'left',
            format: '',
            dataType: 'string',
            allowEditing: false,
            fixed: true
        }
    ];

    groupingDataGrid = $('#groupingTable').dxDataGrid({
        dataSource: getData1(),
        keyExpr: "EQPID",
        columnsAutoWidth: false,
        showBorders: true,
        allowColumnResizing: false,
        selection: {
            mode: "multiple"
        },
        paging: {
            pageSize: 20
        },
        pager: {
            displayMode: 'compact'
        },
        filterRow: {
            visible: true,
            applyFilter: "auto"
        },
        searchPanel: {
            visible: true,
            width: 240,
            placeholder: "Search..."
        },
        headerFilter: {
            visible: true,
            allowSearch: true
        },
        columns: columns,
        editing: {
            mode: "row",
            allowUpdating: false,
            allowDeleting: false,
            useIcons: true
        },
        onRowUpdating: function(e) {
            if (e.newData.hasOwnProperty('GroupName')) {
                e.newData.GroupName = e.newData.GroupName.trim();
                e.newData.IsModify = true;

                let searchItems = groupingData.Groupings.filter(o => o.EQPTYPE == e.oldData.EQPTYPE &&
                    o.EQPID == e.oldData.EQPID &&
                    o.Parameter == e.oldData.Parameter &&
                    o.Chamber == e.oldData.Chamber &&
                    o.GroupName == e.newData.GroupName);
                if (searchItems.length > 0) {
                    showPopup({ "rtnCode": "1234", "rtnMsg": "Group Name Exist !!" });
                    e.cancel = true;
                }
            }
        },
        onRowRemoving: function(e) {
            e.data.Aliasing = 'DEFAULT';
            e.data.GroupName = e.data.Parameter;
            e.data.IsModify = true;

            let selectedItems = groupingDataGrid.getSelectedRowsData();
            for (var i = 0; i < selectedItems.length; i++) {
                let tempSelected = selectedItems[i];
                tempSelected.Aliasing = e.data.Aliasing;
                tempSelected.GroupName = tempSelected.Parameter;
                tempSelected.IsModify = true;
            }

            e.component.refresh(true);
            console.log("RowRemoving");
            e.cancel = true;
        }
    }).dxDataGrid('instance');


    let columns2 = [{
            dataField: 'EQPID',
            caption: 'EQPID',
            width: 120,
            minWidth: 120,
            alignment: 'left',
            format: '',
            dataType: 'string',
            allowEditing: false,
            fixed: true,
            calculateFilterExpression: function (value, operation) {
                var column = this;
                function matchRuleShort(str, rule) {
                    var r = new RegExp(rule.replace("*", ".*")).test(str);
                    debugger;
                    return new RegExp(rule.replace("*", ".*")).test(str);
                }
                if (value) {
                    if (!operation) {
                        return [function (data) {
                            return matchRuleShort(column.calculateCellValue(data), value)
                        }, '=', true];
                    }
                    else {
                        return [column.dataField, operation, value];
                    }
                }
            }
        },
        {
            dataField: 'Parameter',
            caption: 'Parameter',
            width: 330,
            minWidth: 330,
            alignment: 'left',
            format: '',
            dataType: 'string',
            allowEditing: false,
            fixed: true
        }
    ];

    // let groupingHistoryDataGrid = $('#groupingHistoryTable').dxDataGrid({
    //     dataSource: getData2(),
    //     keyExpr: "EQPID",
    //     columnsAutoWidth: false,
    //     showBorders: true,
    //     allowColumnResizing: false,
    //     selection: {
    //         mode: "multiple"
    //     },
    //     paging: {
    //         pageSize: 20
    //     },
    //     pager: {
    //         displayMode: 'compact'
    //     },
    //     filterRow: {
    //         visible: false,
    //         applyFilter: "auto"
    //     },
    //     searchPanel: {
    //         visible: false,
    //         width: 240,
    //         placeholder: "Search..."
    //     },
    //     headerFilter: {
    //         visible: true,
    //         allowSearch: true
    //     },
    //     grouping: {
    //         autoExpandAll: false
    //     },
    //     groupPanel: {
    //         visible: false
    //     },
    //     columns: columns2,
    //     editing: {
    //         mode: "row",
    //         allowUpdating: false,
    //         allowDeleting: false,
    //         useIcons: true
    //     },
    //     onRowUpdating: function(e) {
    //         if (e.newData.hasOwnProperty('GroupName')) {
    //             e.newData.GroupName = e.newData.GroupName.trim();
    //             e.newData.IsModify = true;

    //             let searchItems = groupingData.Groupings.filter(o => o.EQPTYPE == e.oldData.EQPTYPE &&
    //                 o.EQPID == e.oldData.EQPID &&
    //                 o.Parameter == e.oldData.Parameter &&
    //                 o.Chamber == e.oldData.Chamber &&
    //                 o.GroupName == e.newData.GroupName);
    //             if (searchItems.length > 0) {
    //                 showPopup({ "rtnCode": "1234", "rtnMsg": "Group Name Exist !!" });
    //                 e.cancel = true;
    //             }
    //         }
    //     },
    //     onRowRemoving: function(e) {
    //         e.data.Aliasing = 'DEFAULT';
    //         e.data.GroupName = e.data.Parameter;
    //         e.data.IsModify = true;

    //         let selectedItems = groupingDataGrid.getSelectedRowsData();
    //         for (var i = 0; i < selectedItems.length; i++) {
    //             let tempSelected = selectedItems[i];
    //             tempSelected.Aliasing = e.data.Aliasing;
    //             tempSelected.GroupName = tempSelected.Parameter;
    //             tempSelected.IsModify = true;
    //         }

    //         e.component.refresh(true);
    //         console.log("RowRemoving");
    //         e.cancel = true;
    //     }
    // }).dxDataGrid('instance');

    function getData1() {
        return [{
            "RowNumber": "P1 Exhaudst",
            "EQPID": "ATwqaefwqedcfdxr"
        }, {
            "RowNumber": "P1 Exha",
            "EQPID": "ATregaaearhbat"
        }, {
            "RowNumber": "Egdsert Exhaust",
            "EQPID": "ATsfqwef"
        }, {
            "RowNumber": "P1 Exfgdeqst",
            "EQPID": "ATwqagyjkthd"
        }, {
            "RowNumber": ".dx-menu-item.dx-state-focused::after",
            "EQPID": ".dx-datagrid-filter-row"
        }];
    }

    function getData2() {
        return [{
            "EQPID": "P1 Exhaust",
            "Parameter": "AT"
        }, {
            "EQPID": ".dx-menu-item.dx-state-focused::after",
            "Parameter": ".dx-datagrid-filter-row"
        }];
    }
})();