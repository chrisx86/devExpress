// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
;
(function() {

    // $("#gridContainer").dxDataGrid({
    //     dataSource: getData(),
    //     keyExpr: "maiN_AREA",
    //     size: {
    //         width: 150,
    //         height: 35
    //     },
    //     paging: {
    //         pageSize: 10
    //     },
    //     pager: {
    //         showPageSizeSelector: true,
    //         allowedPageSizes: [10, 25, 50, 100]
    //     },
    //     remoteOperations: false,
    //     searchPanel: {
    //         visible: false,
    //         highlightCaseSensitive: true
    //     },
    //     groupPanel: {
    //         visible: true
    //     },
    //     grouping: {
    //         autoExpandAll: false
    //     },
    //     allowColumnReordering: true,
    //     rowAlternationEnabled: true,
    //     showBorders: true,
    //     columns: [{
    //         caption: "MAINAREA",
    //         dataField: "maiN_AREA",
    //         groupIndex: 0
    //     }, {
    //         caption: "EQPTYPE",
    //         dataField: "eqptype",
    //         dataType: "string",
    //         groupIndex: 2
    //     }, {
    //         caption: "EQPID",
    //         dataField: "eqpid",
    //         dataType: "string",
    //         groupIndex: 3
    //     }, {
    //         dataField: "State1",
    //         dataType: "string"
    //     }, {
    //         caption: 'Action',
    //         allowFiltering: false,
    //         allowSorting: false,
    //         width: 100,
    //         cellTemplate: function(container, options) {
    //             let _btnView = $('<div class="view"><i class="fa fa-eye"></i></div>')
    //                 .dxButton({
    //                     type: 'normal',
    //                     stylingMode: 'contained',
    //                     onClick: function(args) {
    //                         alert('options.row.data.State1:', options.row.data.State1);
    //                         console.log('container', container);
    //                         console.log('options', options);
    //                         //<td onclick="javascript:window.location.href-'http://www.stackoverflow.com'" style="cursor:hand">Stackoverflow</td>
    //                         //<input type="button" value="Some text" onclick="window.location.href='<%= Url.Action(" actionName", "controllerName") %>';" />
    //                     }
    //                 });
    //             _btnView.appendTo(container);
    //         }
    //     }, {
    //         caption: 'Buttons',
    //         type: "buttons",
    //         width: 110,
    //         buttons: [{
    //             hint: "Clone",
    //             icon: "repeat",
    //             visible: true,
    //             onClick: function(e) {
    //                 //var clonedItem = $.extend({}, e.row.data, { ID: ++maxID });
    //                 //employees.splice(e.row.rowIndex, 0, clonedItem);
    //                 console.log('e:', e);
    //                 e.component.refresh(true);
    //                 e.event.preventDefault();
    //             }
    //         }, {
    //             icon: "edit",
    //             hint: "Edit",
    //             onClick: function(e) {
    //                 console.log('e:', e);
    //                 //document.location.href = "/expenses/employee/edit?expenseReportId=" + e.row.data.ExpenseReportId;
    //                 e.event.preventDefault();
    //             }
    //         }]
    //     }],
    //     onContentReady: function(e) {
    //         console.log('e:', e)
    //         if (!collapsed) {
    //             collapsed = true;
    //             e.component.expandRow(["P1 Exhaust"]);
    //         }
    //     }
    // });

    var discountCellTemplate = function(container, options) {
        console.log('container', container);
        console.log('options', options);

        $("<div/>").dxBullet({
            onIncidentOccurred: null,
            size: {
                width: 150,
                height: 35
            },
            margin: {
                top: 5,
                bottom: 0,
                left: 5
            },
            showTarget: false,
            showZeroLevel: true,
            value: options.value * 100,
            startScaleValue: 0,
            endScaleValue: 100,
            tooltip: {
                enabled: true,
                font: {
                    size: 18
                },
                paddingTopBottom: 2,
                customizeTooltip: function() {
                    return {
                        text: options.text
                    };
                },
                zIndex: 5
            }
        }).appendTo(container);
    };

    function getData() {
        let DemoData = [{
            "maiN_AREA": "P1 Exhaust",
            "eqptype": "AT",
            "eqpid": "AT-7",
            "State1": "Coupling end"
        }, {
            "maiN_AREA": "P1 Exhaust",
            "eqptype": "AT",
            "eqpid": "AT-8",
            "State1": "Motor Tail end"
        }, {
            "maiN_AREA": "P1 Exhaust",
            "eqptype": "AT",
            "eqpid": "AT-8",
            "State1": "Coupling end"
        }, {
            "maiN_AREA": "P1 Exhaust",
            "eqptype": "AT",
            "eqpid": "AT-9",
            "State1": "Coupling end"
        }, {
            "maiN_AREA": "P1 Exhaust",
            "eqptype": "AT",
            "eqpid": "AT-10",
            "State1": "Motor Tail end"
        }, {
            "maiN_AREA": "P1 Exhaust",
            "eqptype": "AT",
            "eqpid": "AT-10",
            "State1": "Coupling end"
        }, {
            "maiN_AREA": "P1 Exhaust",
            "eqptype": "AT",
            "eqpid": "AT-11",
            "State1": "MotorTail end"
        }, {
            "maiN_AREA": "P1 Exhaust",
            "eqptype": "AT",
            "eqpid": "AT-9",
            "State1": "MotorTail end"
        }, {
            "maiN_AREA": "P1 Exhaust",
            "eqptype": "AT",
            "eqpid": "AT-7",
            "State1": "MotorTail end"
        }, {
            "maiN_AREA": "P1 Exhaust",
            "eqptype": "AT",
            "eqpid": "AT-10",
            "State1": "Fan end"
        }, {
            "maiN_AREA": "P1 Exhaust",
            "eqptype": "AT",
            "eqpid": "AT-11",
            "State1": "Coupling end"
        }, {
            "maiN_AREA": "P1 Exhaust",
            "eqptype": "AT",
            "eqpid": "AT-11",
            "State1": "Fan end"
        }];

        return DemoData;
    }


    let collapsed = false;
})();