using AssetManagementTeam6.API.ErrorHandling;
using AssetManagementTeam6.API.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Net;

namespace AssetManagementTeam6.API.Reports
{
    public partial class ExportExcel
    {
        public MemoryStream ExportDataToStreamForSampleReport(List<ExampleModelsClass> list)
        {
            try
            {
                var ms = new MemoryStream();
                using SpreadsheetDocument ss_doc = SpreadsheetDocument.Create(ms, SpreadsheetDocumentType.Workbook);
                ss_doc.AddWorkbookPart();
                ss_doc.WorkbookPart!.Workbook = new Workbook();
                ss_doc.WorkbookPart.Workbook.Append(new BookViews(new WorkbookView()));
                WorkbookStylesPart workbookStylesPart = ss_doc.WorkbookPart.AddNewPart<WorkbookStylesPart>();
                workbookStylesPart.Stylesheet = GenerateStyleSheetForSampleReport();
                workbookStylesPart.Stylesheet.Save();
                WorksheetPart newWorksheetPart = ss_doc.WorkbookPart.AddNewPart<WorksheetPart>();
                newWorksheetPart.Worksheet = new Worksheet();
                newWorksheetPart.Worksheet.AppendChild(new SheetData());
                var worksheet = newWorksheetPart.Worksheet;
                var sheetData = worksheet.GetFirstChild<SheetData>();

                int numberOfColumns = 7;
                string[] excelColumnNames = new string[numberOfColumns];
                for (int n = 0; n < numberOfColumns; n++)
                    excelColumnNames[n] = GetExcelColumnName(n);

                Columns columns1 = worksheet.GetFirstChild<Columns>() ?? GenerateColumnsForSampleReport();

                worksheet.InsertAfter(columns1, worksheet.GetFirstChild<SheetFormatProperties>());

                uint rowIndex = 1;
                //
                //Title
                //
                var newExcelRow = new Row { RowIndex = rowIndex };
                sheetData!.Append(newExcelRow);
                AppendStyle(excelColumnNames[0] + rowIndex.ToString(), "No.", newExcelRow, 9);
                AppendStyle(excelColumnNames[1] + rowIndex.ToString(), "Name", newExcelRow, 2);
                AppendStyle(excelColumnNames[2] + rowIndex.ToString(), "Age", newExcelRow, 9);
                AppendStyle(excelColumnNames[3] + rowIndex.ToString(), "D.o.B", newExcelRow, 2);
                AppendStyle(excelColumnNames[4] + rowIndex.ToString(), "Score", newExcelRow, 9);
                AppendStyle(excelColumnNames[5] + rowIndex.ToString(), "Remark", newExcelRow, 2);
                AppendStyle(excelColumnNames[6] + rowIndex.ToString(), "Pass/Failed", newExcelRow, 2);

                //
                //Data
                //
                int orderNumber = 0;
                foreach (var item in list)
                {
                    rowIndex++;
                    orderNumber++;
                    newExcelRow = new Row { RowIndex = rowIndex };
                    sheetData.Append(newExcelRow);

                    AppendStyle(excelColumnNames[0] + rowIndex.ToString(), orderNumber.ToString(), newExcelRow, 3, true);
                    AppendStyle(excelColumnNames[1] + rowIndex.ToString(), item.Name, newExcelRow, 3);
                    AppendStyle(excelColumnNames[2] + rowIndex.ToString(), item.Age.ToString(), newExcelRow, 3, true);

                    var actionDate = ConvertToExcelDate(item.DoB);
                    AppendStyle(excelColumnNames[3] + rowIndex.ToString(), actionDate, newExcelRow, 4, true);

                    AppendStyle(excelColumnNames[4] + rowIndex.ToString(), item.Score.ToString(), newExcelRow, 5, true);
                    AppendStyle(excelColumnNames[5] + rowIndex.ToString(), item.Remark, newExcelRow, 6);
                    
                    if (item.Pass)
                        AppendStyle(excelColumnNames[6] + rowIndex.ToString(), "Pass", newExcelRow, 7);
                    else
                        AppendStyle(excelColumnNames[6] + rowIndex.ToString(), "Failed", newExcelRow, 8);

                }
                newWorksheetPart.Worksheet.Save();
                ss_doc.WorkbookPart.Workbook.AppendChild(new Sheets());
                ss_doc.WorkbookPart!.Workbook!.GetFirstChild<Sheets>()!.AppendChild(new Sheet()
                {
                    Id = ss_doc.WorkbookPart.GetIdOfPart(newWorksheetPart),
                    SheetId = 1U,
                    Name = "Sheet 1"
                });
                ss_doc.WorkbookPart.Workbook.Save();

                return ms;
            }
            catch (Exception)
            {
                throw new MyCustomException(HttpStatusCode.InternalServerError, "Export excel for sample data failed");
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
