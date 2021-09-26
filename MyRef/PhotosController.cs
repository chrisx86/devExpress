using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PhotoSharing.Models;
using System.Data.SqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;
using NPOI.XSSF.UserModel;
using Newtonsoft.Json;
using System.Text;
using System.Data;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Configuration;
using System.Data.OleDb;
//using Microsoft.AspNet.Http;
//using Microsoft.AspNetCore.Http;

namespace PhotoSharing.Controllers
{
    //[ValueReporter]
    //4.10 在PhotoController加入 [HandleError(View = "Error")]屬性(Controller層級,會覆蓋Global層級)
    [HandleError(View ="Error")]
    public class PhotosController : Controller
    {
        //2.2建立Data Context 來自 Photo Model 的 PhotoSharingContext 
        PhotoSharingContext context = new PhotoSharingContext();
        /// <summary>
        /// 重寫Controller的Json方法,設定序列化或反序列化時字串的長度為Int32最大值
        /// </summary>
        /// <param name="data"></param>
        /// <param name="contentType"></param>
        /// <param name="contentEncoding"></param>
        /// <param name="behavior"></param>
        /// <returns></returns>
        //protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        //{
        //    return new JsonResult()
        //    {
        //        Data = data,
        //        ContentType = contentType,
        //        ContentEncoding = contentEncoding,
        //        JsonRequestBehavior = behavior,
        //        MaxJsonLength = Int32.MaxValue
        //    };
        //}
        /// <summary>
        /// 工作測試區, GetPhotoList
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetPhotoList()
        {
            var result = context.Photos.ToList();
            return new JsonResult()
            {
                Data = JsonConvert.SerializeObject(result),
                MaxJsonLength = int.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public ActionResult GetCommentList()
        {
            var result = context.Comments.ToList();
            return new JsonResult()
            {
                Data = JsonConvert.SerializeObject(result),
                MaxJsonLength = int.MaxValue,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        //public void GetExcelList(string param1, string param2)
        public ActionResult GetExcelList(string param1, string param2)
        {
            MemoryStream ms = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // 關閉新許可模式通知// 沒設置的話會跳出 Please set the excelpackage.licensecontext property
            var result = context.Photos.ToList();
            //取得資料
            //var result = this.studentService.GetStudentDataList(request).ToList();

            #region NPOI
            //建立Excel NPOI
            //XSSFWorkbook hssfworkbook = new XSSFWorkbook(); //建立活頁簿
            //ISheet sheet = hssfworkbook.CreateSheet("sheet"); //建立sheet

            ////設定樣式
            //ICellStyle headerStyle = hssfworkbook.CreateCellStyle();
            //IFont headerfont = hssfworkbook.CreateFont();
            //headerStyle.Alignment = HorizontalAlignment.Center; //水平置中
            //headerStyle.VerticalAlignment = VerticalAlignment.Center; //垂直置中
            //headerfont.FontName = "微軟正黑體";
            //headerfont.FontHeightInPoints = 20;
            //headerfont.Boldweight = (short)FontBoldWeight.Bold;
            //headerStyle.SetFont(headerfont);

            ////新增標題列
            //sheet.CreateRow(0); //需先用CreateRow建立,才可通过GetRow取得該欄位
            //sheet.AddMergedRegion(new CellRangeAddress(0, 1, 0, 2)); //合併1~2列及A~C欄儲存格
            //sheet.GetRow(0).CreateCell(0).SetCellValue("昕力大學");
            //sheet.GetRow(0).GetCell(0).CellStyle = headerStyle; //套用樣式
            //sheet.CreateRow(2).CreateCell(0).SetCellValue("學生編號");
            //sheet.GetRow(2).CreateCell(1).SetCellValue("學生姓名");
            //sheet.GetRow(2).CreateCell(2).SetCellValue("就讀科系");

            ////填入資料
            //int rowIndex = 3;

            //XSSFHyperlink UrlLink = new XSSFHyperlink(HyperlinkType.Url)
            //{
            //    Address = "https://www.google.com/"
            //};
            //ICell cell = sheet.CreateRow(rowIndex).CreateCell(0);
            //cell.SetCellValue(1);
            //cell.Hyperlink = (UrlLink);

            //sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(2);
            //sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(3);
            #endregion

            //EEPlus
            using (var excel = new ExcelPackage())
            {
                // 建立分頁
                var ws = excel.Workbook.Worksheets.Add("NPW");
                var row = 1;
                int col = 1;
                #region Title
                ws.Cells[row, col++].Value = "PhotoID";
                ws.Cells[row, col++].Value = "Title";
                ws.Cells[row, col++].Value = "UserName";
                #region
                //ws.Cells[row, col++].Value = "Parameter";
                //ws.Cells[row, col++].Value = "SpecHigh";
                //ws.Cells[row, col++].Value = "P95_Value";
                //ws.Cells[row, col++].Value = "P95_UCL";
                //using (var range = ws.Cells[row, col - 3, row, col - 1])
                //{
                //    range.Style.Font.Bold = true; // 粗體
                //    range.Style.Fill.PatternType = ExcelFillStyle.Solid; // 設定背景填色方法，沒有這一行就上背景色會報錯// Solid = 填滿；另外還有斜線、交叉線、條紋等
                //    range.Style.Fill.BackgroundColor.SetColor(Color.LightGreen); // 儲存格顏色
                //}
                //ws.Cells[row, col++].Value = "SpecLow";
                //ws.Cells[row, col++].Value = "P05_Value";
                //ws.Cells[row, col++].Value = "P05_LCL";
                //using (var range = ws.Cells[row, col - 3, row, col - 1])
                //{
                //    range.Style.Font.Bold = true; // 粗體
                //    range.Style.Fill.PatternType = ExcelFillStyle.Solid; // 設定背景填色方法，沒有這一行就上背景色會報錯// Solid = 填滿；另外還有斜線、交叉線、條紋等
                //    range.Style.Fill.BackgroundColor.SetColor(Color.Yellow); // 儲存格顏色
                //}
                //ws.Cells[row, col++].Value = "Lot";
                //ws.Cells[row, col++].Value = "Wafer";
                //ws.Cells[row, col++].Value = "Measure_time";
                //ws.Cells[row, col++].Value = "P95_URL";
                //ws.Cells[row, col++].Value = "P05_URL";
                #endregion
                #endregion
                int j = 1;
                foreach (var PhotoInfo in result)
                {

                    int k = 0;
                    row++;
                    col = 1;
                    ws.Cells[row, col++].Value = PhotoInfo.PhotoID;
                    ws.Cells[row, col++].Value = PhotoInfo.Title;
                    ws.Cells[row, col].Value = PhotoInfo.UserName;
                    ws.Cells[row, col++].AutoFitColumns(); // 欄寬
                    #region
                    //ws.Cells[row, col].Value = outLinerInfo.parameter;
                    //ws.Cells[row, col++].AutoFitColumns(); // 欄寬
                    //ws.Cells[row, col++].Value = JsonObj.spec_high;
                    //ws.Cells[row, col++].Value = JsonObj.percentile_95;
                    //ws.Cells[row, col++].Value = outLinerInfo.p95_ucl;
                    //if (JsonObj.percentile_95 > outLinerInfo.p95_ucl)
                    //{
                    //    using (var range = ws.Cells[row, col - 3, row, col - 1])
                    //    {
                    //        range.Style.Font.Color.SetColor(Color.Red); // 字體顏色
                    //    }
                    //}
                    //ws.Cells[row, col++].Value = JsonObj.spec_low;
                    //ws.Cells[row, col++].Value = JsonObj.percentile_5;
                    //ws.Cells[row, col++].Value = outLinerInfo.p05_lcl;
                    //if (JsonObj.percentile_5 < outLinerInfo.p05_lcl)
                    //{
                    //    using (var range = ws.Cells[row, col - 3, row, col - 1])
                    //    {
                    //        range.Style.Font.Color.SetColor(Color.Red); // 字體顏色
                    //    }
                    //}
                    //ws.Cells[row, col++].Value = JsonObj.lot;
                    //ws.Cells[row, col++].Value = JsonObj.wafer;
                    //ws.Cells[row, col].Value = JsonObj.measure_time;
                    //ws.Cells[row, col++].AutoFitColumns(); // 欄寬

                    //if (JsonObj.percentile_95 > outLinerInfo.p95_ucl)
                    //{
                    //    string URL_Value = domain + string.Format(@"PRODUCT={0}&TEST_TYPE={1}&TEST_PROG={2}&PARAMETER={3}&P95Value={4}&TOGGLE=2", productName, outLinerInfo.test_type, outLinerInfo.test_prog, outLinerInfo.parameter, 95);
                    //    ws.Cells[row, col].Formula = "HYPERLINK(\"" + URL_Value + "\",\"Search\")";
                    //    ws.Cells[row, col++].Style.Font.Color.SetColor(Color.Blue); // 字體顏色
                    //}
                    //else
                    //{
                    //    ws.Cells[row, col++].Value = "";
                    //}

                    //if (JsonObj.percentile_5 < outLinerInfo.p05_lcl)
                    //{
                    //    string URL_Value = domain + string.Format(@"PRODUCT={0}&TEST_TYPE={1}&TEST_PROG={2}&PARAMETER={3}&P95Value={4}&TOGGLE=2", productName, outLinerInfo.test_type, outLinerInfo.test_prog, outLinerInfo.parameter, 5);
                    //    ws.Cells[row, col].Formula = "HYPERLINK(\"" + URL_Value + "\",\"Search\")";
                    //    ws.Cells[row, col++].Style.Font.Color.SetColor(Color.Blue); // 字體顏色
                    //}
                    //else
                    //{
                    //    ws.Cells[row, col++].Value = "";
                    //}


                    //過濾400筆資料,measure_time為前一天之後的rowData
                    //var rawDataJson = JsonConvert.DeserializeObject<JsonModel>(outLinerInfo.rawdata).ChartData.Where(x => Convert.ToDateTime(x.measure_time) >= lastDay);
                    //判斷rawDataJson裡每個值是否有outlier,並判斷spec_high != 0 && spec_low != 0,滿足條件才寫入excel
                    //var rawDataJson = JsonConvert.DeserializeObject<JsonModel>(outLinerInfo.rawdata).ChartData.Where(x => Convert.ToDateTime(x.measure_time) >= lastDay && (x.percentile_95 > outLinerInfo.p95_ucl || x.percentile_5 < outLinerInfo.p05_lcl) && x.spec_high != 0 && x.spec_low != 0 && x.spec_high != x.spec_low);
                    //foreach (var JsonObj in rawDataJson)
                    //{
                    //    chkHasOutlierData = true;
                    //}
                    #endregion
                }
                var stream = new MemoryStream(excel.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", DateTime.Now.ToString("yyyyMMddHHmm") + ".xlsx");  //OK
                //OK
                //return new FileStreamResult(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                //{
                //    FileDownloadName = DateTime.Now.ToString("yyyyMMddHHmm") + ".xlsx"
                //};



                //var excelDatas = new MemoryStream();
                //hssfworkbook.Write(stream);
                //string fileName = @"D:\test.xlsx";
                //FileStream file = new FileStream(fileName, FileMode.Create, System.IO.FileAccess.Write);//產生檔案
                //hssfworkbook.Write(file);
                //file.Close();

                //return File(excelDatas.ToArray(), "application/vnd.ms-excel", DateTime.Now.ToString("yyyyMMddHHmm") + ".xlsx");  //Fail


                try
                {

                    //hssfworkbook.Write(ms);
                    ////return this.File(ms.ToArray(), contentType, "abc.xlsx");

                    //Response.ClearContent();
                    //Response.Buffer = true;

                    //Response.ContentType = "application/ms-excel";
                    //Response.Charset = "";
                    //StringWriter objStringWriter = new StringWriter();
                    //HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
                    //gv.RenderControl(objHtmlTextWriter);
                    //Response.Output.Write(ms.ToArray());
                    //MemoryStream newStream = new MemoryStream(ms.ToArray());


                    //Response.BinaryWrite(stream.ToArray());
                    //Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xlsx");
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //Response.Flush();
                    //Response.End();
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    //hssfworkbook = null;
                    //excelDatas.Close();
                    //excelDatas.Dispose();
                }

                //if (chkHasOutlierData)
                //{
                //產生實體Excel
                //string xlspath = string.Format("{0}LogFiles/{1}.xlsx", AppDomain.CurrentDomain.BaseDirectory, productName + "_plusT");
                //var file = new FileInfo(xlspath); // 檔案路徑
                //if (File.Exists(xlspath))
                //{
                //	File.Delete(xlspath);
                //}                     // 儲存 Excel
                //excel.SaveAs(file);
                //==========================================
                //var stream = new MemoryStream(excel.GetAsByteArray());
                //SendMailtoUser(productName, stream.ToArray());
                //stream.Dispose();
                //}
            }

            //return View();

            //return new FileStreamResult(new MemoryStream(memoryStream.GetBuffer()), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
            //{
            //    FileDownloadName = data.RequestId + ".xlsx"
            //};


            //using (XLWorkbook wb = new XLWorkbook()) //Install ClosedXml from Nuget for XLWorkbook  
            //{
            //    wb.Worksheets.Add(dt);
            //    using (MemoryStream stream = new MemoryStream()) //using System.IO;  
            //    {
            //        wb.SaveAs(stream);
            //        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFile.xlsx");
            //    }
            //}
        }

        [HttpPost]
        public ActionResult UploadExcel(FormCollection post)
        {
            string result = "上傳檔案成功";
            bool isUpload = false;
            List<Comment> PhotoDt = new List<Comment>();
            HttpPostedFileBase httpPostedFile = Request.Files["UploadFileId"];
            string FileName = ReplaceFileName(httpPostedFile.FileName);
            if (httpPostedFile != null && httpPostedFile.ContentLength > (1024 * 1024 * 50))  // 50MB limit  
            {
                ModelState.AddModelError("postedFile", "Your file is to large. Maximum size allowed is 50MB !");
            }
            #region check upload file

            if (httpPostedFile != null && httpPostedFile.ContentLength > 0)
            {
                try
                {
                    //isUpload = chkAllowExtensions(FileName);
                    string filePath = string.Empty;
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    filePath = path + Path.GetFileName(httpPostedFile.FileName);
                    var allowedExtensions = new[] { ".xlsx", ".xls" };
                    var extension = Path.GetExtension(httpPostedFile.FileName);
                    if (!allowedExtensions.Contains(extension.ToLower()))
                    {
                        //code = Constant.DataAccessErr;
                        result = "不支援的檔案格式，只接受(.xlsx)";

                        // 將結果回傳
                        return this.Json(new
                        {
                            code = "9999",
                            result = result
                        },
                        "application/json");
                    }
                    httpPostedFile.SaveAs(filePath);
                    string conString = string.Empty;
                    //string fileLocation = Server.MapPath("~/Uploads/") + FileName;
                    switch (extension)
                    {
                        case ".xls": //For Excel 97-03.  
                            conString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            //excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            break;
                        case ".xlsx": //For Excel 07 and above.  
                            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            break;
                    }
                    #region example
                    try
                    {
                        DataSet ds = new DataSet();
                        DataTable dt = new DataTable();
                        conString = string.Format(conString, filePath);

                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;

                                    //ref: https://www.neerajcodesolutions.com/2017/04/import-excel-sheet-in-database-mvc.html
                                    //Get the name of First Sheet.  
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    if (dtExcelSchema == null)
                                    {
                                        return null;
                                    }
                                    String[] excelSheets = new String[dtExcelSchema.Rows.Count];
                                    int t = 0;
                                    //excel data saves in temp file here.
                                    foreach (DataRow row in dtExcelSchema.Rows)
                                    {
                                        excelSheets[t] = row["TABLE_NAME"].ToString();
                                        t++;
                                    }

                                    //OleDbConnection excelConnection1 = new OleDbConnection(conString);
                                    //string query = string.Format("Select * from [{0}]", excelSheets[0]);
                                    //using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                                    //{
                                    //    dataAdapter.Fill(ds);
                                    //}


                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();

                                    //Read Data from First Sheet.  
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    //cmdExcel.CommandText = "SELECT * From [Photos]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();
                                }
                            }
                        }
                        //ref: https://dotblogs.com.tw/supershowwei/2016/12/09/221622
                        //conString = ConfigurationManager.ConnectionStrings["PhotoSharing"].ConnectionString;
                        //using (SqlConnection con = new SqlConnection(conString))
                        //{
                        //    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                        //    {
                        //        //Set the database table name.  
                        //        sqlBulkCopy.DestinationTableName = "Comments";
                        //        con.Open();
                        //        sqlBulkCopy.WriteToServer(dt);
                        //        con.Close();
                        //        return Json("File uploaded successfully");
                        //    }
                        //}
         
                        foreach (DataRow dr in dt.Rows)
                        {
                            var a = dr.Field<double>("CommentID");
                            var b = dr.Field<string>("Subject");
                            var c = dr.Field<string>("Body");
                            var d = dr.Field<string>("UserName");
                            var e = dr.Field<double>("PhotoID");
                            string sqlstr3 = $@"INSERT INTO Comments ([Subject] ,[Body], [UserName], [PhotoID]) 
                               VALUES ('{b}', '{c}', '{d}', {e})";
                            conString = ConfigurationManager.ConnectionStrings["PhotoSharing"].ConnectionString;
                            using (SqlConnection conn = new SqlConnection(conString))
                            {
                                conn.Open();
                                using (SqlCommand cmd = new SqlCommand(sqlstr3, conn))
                                {
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                    }
                    catch (Exception e)
                    {
                        return Json("error" + e.Message);
                    }
                    #endregion

                    //DataTable tempData = Utility.ImportXLSXToDataTable("template", httpPostedFile.InputStream);
                    //byte[] myByteArray = Enumerable.Repeat((byte)0x08, 10).ToArray();
                    //var csvPhotoDtDt = tempData.AsEnumerable().Select(x => new Photo
                    //{
                    //    Title = "Me standing on top of a mountain",
                    //    Description = "I was very impressed with myself",
                    //    PhotoFile = myByteArray,
                    //    ImageMimeType = "image/jpeg",
                    //    CreatedDate = DateTime.Today,
                    //    UserName = "Fred"
                    //}).ToList();

                    //PhotoDt.AddRange(csvPhotoDtDt);
                    //if (!PhotoDt.Any())
                    //{
                    //    result = " 無資料上傳";
                    //    // 將結果回傳
                    //    return this.Json(new
                    //    {
                    //        code = "9999",
                    //        result = result
                    //    }, "application/json");
                    //}

                    //if ((PhotoDt.Any(x => string.IsNullOrWhiteSpace(x.Description)) || PhotoDt.Any(x => string.IsNullOrWhiteSpace(x.Title)) || PhotoDt.Any(x => string.IsNullOrWhiteSpace(x.PhotoID))))
                    //{
                    //    //code = Constant.DataAccessErr;
                    //    result = "上傳資料缺漏";
                    //    // 將結果回傳
                    //    return this.Json(new
                    //    {
                    //        code = "9999",
                    //        result = result
                    //    }, "application/json");
                    //}
                }
                catch (Exception ex)
                {
                    return this.Json(new
                    {
                        code = "0000",
                        result = ex.Message
                    }, "application/json");
                }
            }

            //#endregion
            //#region check import data(product + parameter)
            //    var PhotoCheckResult = new Photo();
            //    try
            //    {
            //        PhotoCheckResult = productIndexManagementService.CheckUploadDataIsExist(PhotoDt);
            //        if (PhotoCheckResult.unPass.Any())
            //        {
            //            return this.Json(new
            //            {
            //                code = "9999",
            //                errorData = PhotoCheckResult.unPass.ToList(),
            //                importData = PhotoCheckResult.entities
            //            });
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        //logger.Error($"StackTrace: {ex.StackTrace}");
            //        //logger.Error($"InnerException: {ex.InnerException}");
            //        // 將結果回傳
            //        return this.Json(new
            //        {
            //            code = "9999",
            //            result = "檔案寫入失敗"
            //        }, "application/json");
            //    }
            //#endregion

            //#region import data if import data is ok
            //try
            //{
            //    if (PhotoCheckResult.entities.Any())
            //    {
            //        this.productIndexManagementService.SetUploadCsvDataToDB(PhotoCheckResult.entities);
            //        //檢查bigtable是否有資料可寫入
            //        foreach (string product in PhotoCheckResult.entities.Select(x => x.PRODUCT).Distinct())
            //        {
            //            //更新Inline bigtable
            //            this.productIndexManagementService.UpdateInlineData(product);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //logger.Error($"StackTrace: {ex.StackTrace}");
            //    //logger.Error($"InnerException: {ex.InnerException}");
            //    // 將結果回傳
            //    return this.Json(new
            //    {
            //        code = "9999",
            //        result = "檔案寫入失敗"
            //    }, "application/json");
            //}


            #endregion

            return this.Json(new
            {
                code = "0000",
                result = result
            }, "application/json");

            #region
            //if (isUpload)
            //{
            //    string SavePath = Path.Combine(Server.MapPath(RootPath));
            //    fileSrv.chkDIR(SavePath);
            //    int GetFileId = uplSrv.chkDBPaperFileStage1Exist(PaperId, Stage);
            //    if (GetFileId != 0)
            //    {
            //        uplSrv.UpdateStage1PaperFile(httpPostedFile, PaperId, GetFileId, MemberId, FileName, FilePath, SavePath);
            //        paperSrv.UpdatePaperInfo(PaperId, "SubmissionFileId", GetFileId);
            //    }
            //    else
            //    {
            //        fileSrv.chkFileAndReplaceFile(httpPostedFile, SavePath, FileName);
            //        uplSrv.AddPaperFile(httpPostedFile, PaperId, MemberId, FileName, FilePath, SavePath, SubmittedType, Stage);
            //        paperSrv.UpdatePaperInfo(PaperId, "Status", 3);
            //        paperSrv.UpdatePaperInfo(PaperId, "SubmissionFileId", uplSrv.chkDBPaperFileStage1Exist(PaperId, Stage));
            //    }
            //}
            #endregion

        }


        public List<Photo> Import(FormCollection post)
        {
            var list = new List<Photo>();
            //HttpPostedFileBase httpPostedFile = Request.Files["UploadFileId"];
            //string FileName = ReplaceFileName(httpPostedFile.FileName);
            using(var stream = new MemoryStream())
            {
                //await file.CopyToAsync(stream);
                using(var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 0; row < rowcount; row++)
                    {
                        list.Add(new Photo
                        {
                            Title = worksheet.Cells[row,1].Value.ToString().Trim(),
                            UserName = worksheet.Cells[row, 2].Value.ToString().Trim(),
                            Description = worksheet.Cells[row, 3].Value.ToString().Trim()
                        });
                    }
                }
            }
            return list;
        }

        //        [HttpPost]
        //        public ActionResult UploadExcelsheet()
        //        {
        //            if (Request.Files.Count > 0)
        //            {
        //                var file = Request.Files[0];
        //                List<ProductModel> _lstProductMaster = new List<ProductModel>();
        //                string filePath = string.Empty;
        //                if (Request.Files != null)
        //                {
        //                    string path = Server.MapPath("~/Uploads/Product/");
        //                    if (!Directory.Exists(path))
        //                    {
        //                        Directory.CreateDirectory(path);
        //                    }
        //                    filePath = path + Path.GetFileName("ProductUploadSheet-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + Path.GetExtension(file.FileName));
        //                    string extension = Path.GetExtension("ProductUploadSheet-" + DateTime.Now.ToString("dd-MMM-yyyy-HH-mm-ss-ff") + Path.GetExtension(file.FileName));
        //                    file.SaveAs(filePath);

        //                    string conString = string.Empty;
        //                    switch (extension)
        //                    {
        //                        case ".xls": //Excel 97-03.
        //                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
        //                            break;
        //                        case ".xlsx": //Excel 07 and above.
        //                            conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
        //                            break;
        //                    }
        //                    int total = 0;
        //                    int entered = 0;
        //                    int failed = 0;

        //                    conString = string.Format(conString, filePath);

        //                    using (OleDbConnection connExcel = new OleDbConnection(conString))
        //                    {
        //                        using (OleDbCommand cmdExcel = new OleDbCommand())
        //                        {
        //                            using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
        //                            {
        //                                DataTable dt = new DataTable();
        //                                cmdExcel.Connection = connExcel;

        //                                //Get the name of First Sheet.
        //                                connExcel.Open();
        //                                DataTable dtExcelSchema;
        //                                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
        //                                string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
        //                                connExcel.Close();

        //                                //Read Data from First Sheet.
        //                                connExcel.Open();
        //                                cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
        //                                odaExcel.SelectCommand = cmdExcel;
        //                                odaExcel.Fill(dt);
        //                                connExcel.Close();


        //                                if (dt.Rows.Count > 0)
        //                                {

        //                                    foreach (DataRow row in dt.Rows)
        //                                    {
        //                                        total++;
        //                                        _lstProductMaster.Add(new ProductModel
        //                                        {
        //                                            ProductName = row["ProductName"].ToString().Replace("'", "''"),
        //                                            ProductSKU = row["VendorSKU"].ToString().Trim() + "GL" + DateTime.Now.Year.ToString().Substring(2),
        //                                            VendorSKU = row["VendorSKU"].ToString().Trim(),
        //                                            DisplayText = row["DisplayText"].ToString().Trim(),
        //                                            ProductSpecification = row["ProductSpecification"].ToString().Replace("'", "''"),
        //                                            Description = row["Description"].ToString().Replace("'", "''"),
        //                                            ShortDescription = row["ShortDescription"].ToString().Replace("'", "''"),
        //                                            LongDescription = row["LongDescription"].ToString().Replace("'", "''"),
        //                                            InventoryCount = row["InventoryCount"].ToString().Trim(),
        //                                            ListPrice = row["ListPrice"].ToString().Trim(),
        //                                            SellingPrice = row["SellingPrice"].ToString().Trim(),
        //                                            // if (chkMultiple.Checked == true && ProductSKU != "")
        //                                            //{
        //                                            //    ProductImage = Convert.ToString(VendorSKU).Trim() + "_1.jpg";
        //                                            //}
        //                                            //else if ((chkMultiple.Checked == false && details.SKU != ""))
        //                                            //{
        //                                            //    ProductImage = Convert.ToString(VendorSKU).Trim() + ".jpg";
        //                                            //}
        //                                        });
        //                                        entered++;
        //                                        if (entered > 0)
        //                                        {
        //                                            GetOccassionRecipientMasters();
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            failed = total - entered;
        //                            if (failed > 0)
        //                            {
        //                                ViewBag.Fail = failed + " Records not entered";
        //                            }
        //                            else
        //                            {
        //                                ViewBag.Pass = entered + " Records entered";
        //                                ViewBag.Fail = failed + " Records not entered";


        //                            }
        //                            ViewBag.Total = total + " Total Records";

        //                        }
        //                    }
        //                }
        //                List<ProductMaster> _productmaster = new List<ProductMaster>();
        //                ViewBag.maindata = _lstProductMaster;
        //                //return Json(_lstProductMaster, JsonRequestBehavior.AllowGet);
        //                return View("ImportProductsFromExcel", _lstProductMaster);
        //            }
        //            else
        //            {
        //                //return Json("", JsonRequestBehavior.AllowGet);
        //                return View();
        //            }

        //        }
        //    }
        //}




        public bool chkAllowExtensions(string fileName)
        {
            bool IsAllowUpload = false;
            string[] allowExtensions = { ".xlsx", ".xls" };
            string subFileName = Path.GetExtension(fileName).ToLower();
            for (int j = 0; j < allowExtensions.Length; j++)
            {
                if (subFileName == allowExtensions[j])
                {
                    IsAllowUpload = true;
                    break;
                }
            }
            return IsAllowUpload;
        }
        public string ReplaceFileName(string fileName)
        {
            return fileName.Replace("+", "").Replace("%", "").Replace(" ", "").Replace("!", "").Replace(";", "").Replace(":", "").Replace(",", "").Replace("#", "").Replace("-", "").Replace("*", "").Replace("@", "").Trim();
        }

        //public async Task<List<Photo>> Import(IFormFile file)
        //{

        //}

        //[HttpPost]
        //public ActionResult PaperFileUpload(FormCollection post)
        //{
        //    if (Request.Files.AllKeys.Any())
        //    {
        //        UploadDBService uplSrv = new UploadDBService();
        //        FileService fileSrv = new FileService();
        //        PaperFile paperFile = new PaperFile();
        //        HttpPostedFileBase httpPostedFile = Request.Files["UploadFileId"];
        //        string RootPath = "~/Areas/Users/SubmissionFiles/";
        //        string FilePath = "";
        //        string WebRootPath = Request.PhysicalApplicationPath;
        //        int ConferenceId = Convert.ToInt32(post["ConferenceId"]);
        //        int PaperId = Convert.ToInt32(post["paperId"]);
        //        string SubmittedType = post["SubmittedType"].ToString();
        //        string SetDIR = ConferenceId + "/" + PaperId;
        //        string FileName = fileSrv.ReplaceFileName(httpPostedFile.FileName);
        //        string SubFileName = Path.GetExtension(FileName).ToLower();
        //        string MemberId = Session["MemberId"].ToString();
        //        int Stage = 1;
        //        bool isUpload = false;
        //        switch (SubmittedType)
        //        {
        //            case "AUP":
        //                RootPath += SetDIR + "/" + SubmittedType + "/";
        //                FilePath = SetDIR + "/" + SubmittedType + "/" + FileName;
        //                break;
        //            case "RUP":
        //                RootPath += SetDIR + "/" + SubmittedType + "/";
        //                FilePath = SetDIR + "/" + SubmittedType + "/" + FileName;
        //                break;
        //        }

        //        try
        //        {
        //            if (httpPostedFile != null && httpPostedFile.ContentLength != 0)
        //            {
        //                isUpload = fileSrv.chkAllowExtensions(FileName);
        //                if (isUpload)
        //                {
        //                    string SavePath = Path.Combine(Server.MapPath(RootPath));
        //                    fileSrv.chkDIR(SavePath);
        //                    int GetFileId = uplSrv.chkDBPaperFileStage1Exist(PaperId, Stage);
        //                    if (GetFileId != 0)
        //                    {
        //                        uplSrv.UpdateStage1PaperFile(httpPostedFile, PaperId, GetFileId, MemberId, FileName, FilePath, SavePath);
        //                        paperSrv.UpdatePaperInfo(PaperId, "SubmissionFileId", GetFileId);
        //                    }
        //                    else
        //                    {
        //                        fileSrv.chkFileAndReplaceFile(httpPostedFile, SavePath, FileName);
        //                        uplSrv.AddPaperFile(httpPostedFile, PaperId, MemberId, FileName, FilePath, SavePath, SubmittedType, Stage);
        //                        paperSrv.UpdatePaperInfo(PaperId, "Status", 3);
        //                        paperSrv.UpdatePaperInfo(PaperId, "SubmissionFileId", uplSrv.chkDBPaperFileStage1Exist(PaperId, Stage));
        //                    }
        //                }
        //            }

        //            if (isUpload)
        //            {
        //                UploadViewModel ulv = new UploadViewModel();
        //                ulv.DataList = uplSrv.GetStagePaperFile(PaperId, Stage);
        //                List<PaperFile> oStr = new List<PaperFile>();
        //                foreach (PaperFile u in ulv.DataList)
        //                {
        //                    oStr.Add(new PaperFile() { SysPath = u.SysPath, FileName = u.FileName, FileId = u.FileId, UploadDate = Convert.ToDateTime(u.UploadDate) });
        //                }
        //                publicSrv.AddLog(Session["MemberId"].ToString(), "PaperUpload", "稿件上傳成功", ConferenceId, PaperId);
        //                string s = JsonConvert.SerializeObject(oStr);
        //                return Json(s, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                return Json(new { isUploaded = isUpload, result = "" }, "text/html");
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            publicSrv.AddLog(Session["MemberId"].ToString(), "PaperUploadError", "稿件上傳錯誤" + e.ToString(), ConferenceId, PaperId);
        //            throw;
        //        }
        //    }
        //    return Json(new { isUploaded = false, result = "" }, "text/html");
        //}



        //2.3-1 Index()回傳Photo
        // [ValueReporter]
        public ActionResult Index()
        {
            ViewBag.Date = DateTime.Now; //用ViewBag帶入今日日期
            //return View(context.Photos.ToList());
            return View();
        }

        //2.3-2 Display(int id) 透過id參數回傳Photo,若找不到回傳HttpNotFound()helper
        //[ValueReporter]
        public ActionResult Display(int? id)
        {
            ViewData["Date"] = DateTime.Now;  //用ViewData帶入今日日期
            if (id==null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Photo photo = context.Photos.Find(id);
            if (photo == null) return HttpNotFound();
            return View(photo);
        }

        //2.3-3 GET:Create() 產生新增Photo作業,並回傳 new Photo()並產生CreatedDate屬性值= DateTime.Today
        public ActionResult Create()
        {
            Photo newPhoto = new Photo();
            newPhoto.CreatedDate = DateTime.Today;
            return View(newPhoto);
        }

        //public ActionResult Create2(Photo p)
        //{
        //    Photo newPhoto = new Photo();
        //    newPhoto.CreatedDate = DateTime.Today;
        //    return View(newPhoto);
        //}
        [HttpPost]
        //public ActionResult JqueryPost(string type, List<Photo> model, List<Comment> comment)
        public ActionResult JqueryPost(List<JsonPostModel> formPosts)
        {
            Photo newPhoto = new Photo();
            newPhoto.CreatedDate = DateTime.Today;
            return View("Create", newPhoto);
        }
        [HttpPost]
        //public ActionResult JqueryPost(string chk, List<Photo> model, List<Comment> comment)
        //public ActionResult JqueryPost2(List<NewClass> formPosts)
        public ActionResult JqueryPost2(NewClass formPosts)
        {
            Photo newPhoto = new Photo();
            newPhoto.CreatedDate = DateTime.Today;
            return View("Create", newPhoto);
        }


        [HttpPost]
        public ActionResult Index(JsonPostModel model)
        {
            Photo newPhoto = new Photo();
            newPhoto.CreatedDate = DateTime.Today;
            return View("Create", newPhoto);
        }

        [HttpPost]
        public ActionResult myTest1(RootContainer rootContainer)
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult myTest2(RootContainer rootContainer)
        {
            return View("Create");
        }
        [HttpPost]
        public ActionResult myTest3(List<RootContainer3> RootContainer3)
        {
            var RootContainer = RootContainer3;
            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    for (int i = 0; i < RootContainer[0].photos.Count(); i++)
                    {
                        RootContainer[0].photos[i].CreatedDate = DateTime.Now;
                        context.Photos.Add(RootContainer[0].photos[i]);
                        context.SaveChanges();
                    }
                    for (int i = 0; i < RootContainer[0].comments.Count(); i++)
                    {
                        RootContainer[0].comments[i].PhotoID = 1;
                        context.Comments.Add(RootContainer[0].comments[i]);
                    }
                    context.SaveChanges();
                    //throw new ArgumentNullException("message");
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                }
            }
            return View("Create");
        }
        //RootContainer[0].photos[i].PhotoID = i*10;
        //RootContainer[0].photos[i].Title = "title" + i;
        //RootContainer[0].photos[i].Description = "Description" + i;
        //RootContainer[0].photos[i].UserName = "UserName" + i;
        //RootContainer[0].comments[i].Subject = "subject" + 1;
        //RootContainer[0].comments[i].Body = "Body" + i;
        //RootContainer[0].comments[i].UserName = "UserName" + i;
        //2.3-4 Post:Create(Photo photo, HttpPostedFileBase image),使用HTTP POST,執行新增Photo回存作業,回傳RedirectToAction()helper
        //      如果ModelState.IsValid==false,回傳Photo給View, 反之執行新增Photo回存作業
        //      photo.ImageMimeType = image.ContentType;
        //      photo.PhotoFile = new byte[image.ContentLength];
        //      image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
            photo.CreatedDate = DateTime.Today;

            //if (ModelState.IsValid)
            //{
            //    //有post照片才做照片上傳的處理
            //    if (image!=null)
            //    {
            //        photo.ImageMimeType = image.ContentType;
            //        photo.PhotoFile = new byte[image.ContentLength];
            //        image.InputStream.Read(photo.PhotoFile,0, image.ContentLength);
            //    }

            //    context.Photos.Add(photo);
            //    context.SaveChanges();

            //    return RedirectToAction("Index");
            //}

            return View("Create", photo);
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create2(Photo photo, HttpPostedFileBase image)
        //{
        //    try
        //    {
        //        photo.CreatedDate = DateTime.Today;
        //        if (ModelState.IsValid)
        //        {
        //            //有post照片才做照片上傳的處理
        //            if (image != null)
        //            {
        //                photo.ImageMimeType = image.ContentType;
        //                photo.PhotoFile = new byte[image.ContentLength];
        //                image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
        //            }
        //            context.Photos.Add(photo);
        //            context.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        // Attempt to commit the transaction.
        //        transaction.Commit();
        //        return View("Create", photo);
        //    }
        //    catch (Exception)
        //    {
        //        try
        //        {
        //            transaction.Rollback();
        //        }
        //        catch (Exception ex2)
        //        {
        //            Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
        //            Console.WriteLine("  Message: {0}", ex2.Message);
        //            return View("Create", photo);
        //        }
        //    }

        //}
        //2.3-5 Get:Delete(int id)產生刪除Photo作業,並回傳Photo(),若找不到回傳HttpNotFound()helper
        //[ValueReporter]
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Photo photo = context.Photos.Find(id);
            if (photo == null) return HttpNotFound();
            return View(photo);
        }

        //2.3-6 Post:建立DeleteConfirmed(int id)使用[ActionName("Delete")]屬性,透過HTTP POST,執行刪除Photo回存作業.回傳RedirectToAction()helper
        [HttpPost,ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Photo photo = context.Photos.Find(id);
            context.Photos.Remove(photo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        //2.3-7 建立GetImage(int id)回傳File(photo.PhotoFile, photo.ImageMimeType)
        //這不是View()Helper的ActionResult, 這是File()helper
        //3.13-3 在PhotoController中的GetImage Action上標註排除使用Filter
        //[ValueReporter(IsCheck=false)]
        public FileContentResult GetImage(int id)
        {
            Photo photo = context.Photos.Find(id);
            return File(photo.PhotoFile, photo.ImageMimeType);
        }

        public ActionResult BSTest()
        {
            return View(context.Photos.ToList());
        }

        //3.6.在PhotoController.cs建立[ChildActionOnly] _PhotoGallery action,用於Partial View
        [ChildActionOnly]
        public ActionResult _PhotoGallery(int number=0)
        {
            List<Photo> photos;
            if (number == 0)
            {
                photos = context.Photos.OrderByDescending(p => p.CreatedDate).ThenByDescending(p => p.PhotoID).ToList();
                
                //LINQ
                //var photos = (from p in context.Photos
                //              orderby p.CreatedDate descending, p.PhotoID descending
                //              select p).ToList();

                //SQL
                // select * from Photos
                // order by CreatedDate desc,PhotoID desc
            }
            else
            {
                photos = context.Photos.OrderByDescending(p => p.CreatedDate).ThenByDescending(p => p.PhotoID).Take(number).ToList();
                
                //LINQ
                //var photos = (from p in context.Photos
                //              orderby p.CreatedDate descending, p.PhotoID descending
                //              select p).Take(number).ToList();

                //SQL
                // select top 2 * from Photos
                // order by CreatedDate desc,PhotoID desc
            }
            return PartialView(photos);
        }

        public ActionResult ExceptionDemo()
        {
            try
            {
                int a = 0;
                int c = 10 / a;
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

        [HandleError(View = "Error2")]
        public ActionResult SliderShow()
        {
            throw new NotImplementedException();
        }

        [Route(@"Photos/Title/{title}")]
        public ActionResult DisplayByTitle(string title)
        {
            Photo photo = context.Photos.Where(p => p.Title == title).FirstOrDefault();
            if (photo == null) return HttpNotFound();
            return View("Display",photo);
        }
        public ActionResult ExportExcelTest()
        {
            //取得資料
            //var result = this.studentService.GetStudentDataList(request).ToList();

            //建立Excel
            XSSFWorkbook hssfworkbook = new XSSFWorkbook(); //建立活頁簿
            ISheet sheet = hssfworkbook.CreateSheet("sheet"); //建立sheet

            //設定樣式
            ICellStyle headerStyle = hssfworkbook.CreateCellStyle();
            IFont headerfont = hssfworkbook.CreateFont();
            headerStyle.Alignment = HorizontalAlignment.Center; //水平置中
            headerStyle.VerticalAlignment = VerticalAlignment.Center; //垂直置中
            headerfont.FontName = "微軟正黑體";
            headerfont.FontHeightInPoints = 20;
            headerfont.Boldweight = (short)FontBoldWeight.Bold;
            headerStyle.SetFont(headerfont);

            //新增標題列
            sheet.CreateRow(0); //需先用CreateRow建立,才可通过GetRow取得該欄位
            sheet.AddMergedRegion(new CellRangeAddress(0, 1, 0, 2)); //合併1~2列及A~C欄儲存格
            sheet.GetRow(0).CreateCell(0).SetCellValue("昕力大學");
            sheet.GetRow(0).GetCell(0).CellStyle = headerStyle; //套用樣式
            sheet.CreateRow(2).CreateCell(0).SetCellValue("學生編號");
            sheet.GetRow(2).CreateCell(1).SetCellValue("學生姓名");
            sheet.GetRow(2).CreateCell(2).SetCellValue("就讀科系");

            //填入資料
            int rowIndex = 3;

            XSSFHyperlink UrlLink = new XSSFHyperlink(HyperlinkType.Url)
            {
                Address = "https://www.google.com/"
            };
            ICell cell = sheet.CreateRow(rowIndex).CreateCell(0);
            cell.SetCellValue(1);
            cell.Hyperlink = (UrlLink);

            sheet.GetRow(rowIndex).CreateCell(1).SetCellValue(2);
            sheet.GetRow(rowIndex).CreateCell(2).SetCellValue(3);



            var excelDatas = new MemoryStream();
            
            hssfworkbook.Write(excelDatas);
            string fileName = @"D:\test.xlsx";

            FileStream file = new FileStream(fileName, FileMode.Create, System.IO.FileAccess.Write);//產生檔案
            hssfworkbook.Write(file);
            file.Close();


            return File(excelDatas.ToArray(), "application/vnd.ms-excel", DateTime.Now.ToString("yyyyMMddHHmm") + ".xlsx");
            //return File(excelDatas.ToArray(), "application/vnd.ms-excel", fileName);
            //return View();
        }

        public ActionResult JQDataTable()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetList()
        {
            List<Photo> photos;
            photos = context.Photos.ToList<Photo>();
            int totalrows = photos.Count();
            //Server Side Parameter
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            //string searchValue = Request["columns[0][search][value]"];
            //string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            if (!string.IsNullOrEmpty(searchValue))
            {
                photos = photos.Where(x => x.Description.ToLower().Contains(Request["columns[0][search][value]"].ToLower())).ToList<Photo>();
            }
            if (!string.IsNullOrEmpty(Request["columns[0][search][value]"]))
                photos = photos.Where(x => x.Description.ToLower().Contains(Request["columns[0][search][value]"].ToLower())).ToList<Photo>();
            //sorting
            //photos = photos.OrderBy(sortColumnName + " " + sortDirection).ToList<Photo>();

            //paging
            photos = photos.Skip(start).Take(length).ToList<Photo>();
            int totalrowsafterfiltering = photos.Count();
            return Json(new
            {
                data = JsonConvert.SerializeObject(photos),
                draw = Request["draw"],
                recordsTotal = totalrows,
                recordsFiltered = totalrowsafterfiltering,
            }, JsonRequestBehavior.AllowGet);
            //return Content(JsonConvert.SerializeObject(photos, Formatting.Indented,
            //   new JsonSerializerSettings
            //   {   //視自己需求可以拿掉
            //       ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //   }), "application/json");
        }

        public ActionResult CSVOperator()
        {
            return View();
        }
        public ActionResult CSVUpload()
        {
            return View();
        }
    }
}


//7 用ajax的方式來實現回覆留言的功能

//7.1 加入CommentController(Mvc5 Controller-Empty)
//   在CommentController加入_Create Action
//   在_CreateAComment View使用AJAX


//7.2 加入CommentController(Mvc5 Controller-Empty)
//7.2-1 宣告PhotoSharingContext()並在建構式建構DBContext物件
//7.2-2 加入_CommentsForPhoto Action
//7.2-3 加入_CommentsForPhoto Action的Partial View
//      使用Scaffold template:Empty
//      Model Class:Comment (PhotoSharing.Models)
//      Data Context Class:PhotoSharingContext (PhotoSharing.Models)
//      勾選Create as a partial view核取方塊
//7.2-4 將_CommentsForPhoto.cshtml移至Shared目錄

//
//7.3 在/Shared/_CommentsForPhoto.cshtml加入IEnumerable<T>及foreach (var item in Model){}

//7.4 在/Photo/Display View加入@Html.Action("_CommentsForPhoto", "Comment", new { PhotoID = Model.PhotoID })

//可先測試一下在Display中是否能正常顯示回覆留言的PartialView


// 使用NuGet安裝套件 
// 1.*Jquery(如果需要)
// 2.Microsoft.jQuery.Unobtrusive.Ajax

//7.5-1 加入_Create Action

//7.5-2 加入_Create Action的_CreateAComment View
//      使用Scaffold template:Empty
//      Model Class:Comment (PhotoSharing.Models)
//      Data Context Class:PhotoSharingContext (PhotoSharing.Models)
//      勾選Create as a partial view核取方塊

//7.5-3 將_CreateAComment.cshtml移至Shared目錄
// GET: /Comment/_Create. A Partial View for displaying the create comment tool as a AJAX partial page update

//7.6-1 在CommentController加入_CommentsForPhoto POST Action

//7.6-2 /Shared/_CreateAComment.cshtml加入Html Helper 及 class屬性

//7.6-3 /Shared/_CommentsForPhoto.cshtml使用Ajax.BeginForm

//7.6-4 /Shared/_CommentsForPhoto.cshtml加入@Html.Action("_Create", "Comment", new { PhotoID = ViewBag.PhotoId })
//      透過@Html.Action加入_CreateAComment Partial View

//7.7 在_MainLayout.cshtml加入SCRIPT
//7.7-1 加入*jquery-3.1.1.min.js(如果需要的話)(注意:不可用jquery-1.XXX版本)

//7.7-2 *****加入jquery.unobtrusive-ajax.min.js*****這個一定要記得安裝啊!!!!!!

//到這裡可以先測試一下在Display中是否能以Ajax方式新增留言




