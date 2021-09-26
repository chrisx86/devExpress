(($) => {
    console.log($);
    let SelectBox1 = null;
    let SelectBox2 = null;
    SelectBox2 = $('#SelectBox2').dxSelectBox({
        //dataSource: dataArr,
        searchEnabled: true,
        acceptCustomValue: true,
        wrapItemText: true,
        onValueChanged: (e) => {
            //console.log(e);
            let selectValue = e.value.trim();
            //console.log(selectValue);
        }
    }).dxSelectBox("instance");

    $.ajax({
        url: 'GetPhotoList',
        dataType: 'json'
    }).done((data, textStatus) => {
        //console.log('data', data);
        let jsdata = JSON.parse(data);
        //console.log('jsdata', jsdata);
        let dataArr = [];
        $.each(jsdata, (i, item) => {
            //console.log(i);
            //console.log(item.Title);
            dataArr.push(item.Title);
        });
        //sendResponse({ farewell: "ok" });
        SelectBox1 = $('#SelectBox1').dxSelectBox({
            dataSource: dataArr,
            searchEnabled: true,
            acceptCustomValue: true,
            wrapItemText: true,
            onValueChanged: (e) => {
                //console.log(e);
                let selectValue = e.value.trim();
                //console.log(selectValue);
            }
        }).dxSelectBox("instance");

        //var source = new DevExpress.data.DataSource({
        //    store: data.data,
        //    paginate: true,
        //    pageSize: 10
        //});
        //inputGroupingName.option('dataSource', source);
    }).fail((jqXHR, textStatus, errorThrown) => {
        console.log('fail:', jqXHR, textStatus, errorThrown);
    });

    //Get SelectBox2
    $.ajax({
        url: 'GetCommentList',
        dataType: 'json'
    }).done((data, textStatus) => {
        //console.log('data', data);
        let jsdata = JSON.parse(data);
        //console.log('jsdata', jsdata);
        let dataArr = [];
        $.each(jsdata, (i, item) => {
            //console.log(i);
            //console.log(item.Subject);
            dataArr.push(item.Subject);
        });

        var source = new DevExpress.data.DataSource({
            store: dataArr,
            paginate: true,
            pageSize: 10
        });
        SelectBox2.option('dataSource', source);

    }).fail((jqXHR, textStatus, errorThrown) => {
        console.log('fail:', jqXHR, textStatus, errorThrown);
    });

    $('#btn_ExcelDownload').off('click').on('click', (e) => {
        e.preventDefault();
        let param1 = SelectBox1.option('value') == null ? '' : SelectBox1.option('value').trim();
        let param2 = SelectBox2.option('value') == null ? '' : SelectBox2.option('value').trim();

        if (param1 != '' && param2 != '') {
            console.log('param1', param1);
            console.log('param2', param2);
            window.location = "GetExcelList?param1=111&param2=2222";
        } else {
            console.log('none');
        }

        //下載WAT template
        //$('#btnWatTemplate').on('click', function () {
        //    window.location = contextPath + 'ProductIndexManagement/TemplateCSV?type=WAT';
        //});

        //用ajax不能從browser匯出excel
        //$.ajax({
        //    type: 'POST',
        //    url: 'GetExcelList'

        //}).done((r) => {
        //    console.log('abc');
        //}).fail(() => {
        //    console.log('ddd');
        //});
    });


    $('#btn_UploadExcel').off('click').on('click', (e) => {
        e.preventDefault();
        let fileInput = document.getElementById('fileUpload');
        if (fileInput.files.length == 0) { return; }
        let data = new FormData();
        var fl = $("#fileUpload").get(0).files[0];
        if (fl != undefined) {
            data.append("UploadFileId", fl);
        }
        for (let i = 0; i < fileInput.files.length; i++) {
            data.append(fileInput.files[i].name, fileInput.files[i]);
        }
        console.log(data);

        //var Url = Path("Photos", "UploadExcel");
        //console.log('Url', Url);
        $.ajax({
            async: false,
            type: "POST",
            url: 'UploadExcel',
            contentType: false,
            processData: false,
            dataType: "json",
            data: data
        }).done(data => {
            console.log(data);
            //if (data.code == "9999") {
            //    events.genCheckResultAreaItem("Inline", data.errorData);

            //    importData = data.importData;

            //    fileInput.files = null;
            //    $('#inlineFile').val('');

            //    $("#progress").hide();
            //    $("#ulpoadArea").hide();
            //    $("#checkResultArea").show();

            //    events.setBtnContinueImportTypeToInline();
            //} else {
            //    if (data.code != '0000') {
            //        alert(data.result);
            //        try {
            //            fileInput.files = null;
            //        } catch (ex1) { }
            //        $("#progress").hide();
            //        $('#inlineFile').val('');
            //        return;
            //    }
            //    $("#progress").hide();
            //    alert(data.result);
            //    $('#inlineFile').val('');
            //}
        }).fail(() => {
            try {
                fileInput.files = null;
            } catch (ex1) {

            }
            $('#fileUpload').val('');
            $("#progress").hide();
        });

    });

})(jQuery);


//if ($("#excelfile").val() != "") {
//    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$/;
//    /*Checks whether the file is a valid excel file*/
//    if (!regex.test($("#excelfile").val().toLowerCase())) {
//        alert("Please upload a valid Excel file!");
//        return false;
//    }
//    else {
//        UploadSelectedExcelsheet();
//    }
//}
//else
//{
//    alert("Please upload a Excel file!");
//    return false;
//}


//function UploadSelectedExcelsheet() {
//    var data = new FormData();
//    var i = 0;
//    var fl = $("#excelfile").get(0).files[0];

//    if (fl != undefined) {

//        data.append("file", fl);

//    }
//    var Url = Path("ProductBulk", "UploadExcelsheet");

//    $.ajax({
//        type: "POST",
//        url: Url,
//        contentType: false,
//        processData: false,
//        data: data,
//        success: function (result) {

//            $("#Products").html(result);

//            $('.ddlRecipient').multiselect({
//                includeSelectAllOption: true
//            });
//            $('.ddlOccasionmaster').multiselect({
//                includeSelectAllOption: true
//            });

//        },
//        error: function (xhr, status, p3, p4) {
//            var err = "Error " + " " + status + " " + p3 + " " + p4;
//            if (xhr.responseText && xhr.responseText[0] == "{")
//                err = JSON.parse(xhr.responseText).Message;
//            alert(err);
//            return false;
//        }
//    });
//}





//$('#btnUploadFile').on('click', function () {
//    var data = new FormData();
//    var files = $("#fileUpload").get(0).files;
//    if (files.length > 0) {
//        data.append("UploadFileId", files[0]);
//        data.append("ConferenceId", $("#ConferenceId").val());
//        data.append("paperId", $("#paperId").val());
//        data.append("SubmittedType", $("#SubmittedType").val());
//    }

//    var ajaxRequest = $.ajax({
//        type: "POST",
//        url: "@Url.Action("PaperFileUpload")",
//        contentType: false,
//        processData: false,
//    dataType: "json",
//    async: false,
//    data: data,
//    success: function (data) {
//        var htmlresult = "";
//        if (data.isUploaded || data.length > 0) {
//            var jsdata = JSON.parse(data);
//            $.each(jsdata, function (i, item) {
//                htmlresult += "己上傳成功：";
//                htmlresult += "<a href='/Users/Paper/DownloadFile?fId=" + item.FileId + "'>" + item.FileName + "</a>&nbsp;&nbsp;";
//                htmlresult += "上傳日期：";
//                htmlresult += item.UploadDate.slice(0, 10);
//                htmlresult += "<hr />";
//            });
//            $("#DivResult").empty();
//            $("#DivResult").append(htmlresult);
//            $("#Confirm").show();
//            alert("稿件己上傳成功！");
//        } else {
//            alert("無法上傳此檔案類型，請依規定上傳！");
//            $("#Confirm").hide();
//        }
//    },
//    error: function () {
//        alert("無法上傳此檔案類型，請依規定上傳！");
//        $("#Confirm").hide();
//    }
//});
//});

//$.ajax({
//    type: "POST",
//    url: "ExportToExcel",
//    contentType: "application/json; charset=utf-8",
//    data: JSON.stringify({
//        selecteddate: dateText
//    }),
//    error: function (result) {
//        alert('Oh no: ' + result.responseText);
//    }
//});