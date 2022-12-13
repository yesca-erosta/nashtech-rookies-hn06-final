using AssetManagementTeam6.API.ErrorHandling;
using AssetManagementTeam6.Data.Entities;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Net;

namespace AssetManagementTeam6.API.Reports
{
    public partial class CustomExportExcel
    {
        public MemoryStream ExportDataToStream(List<Report> list)
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
                AppendStyle(excelColumnNames[0] + rowIndex.ToString(), "Category", newExcelRow, 2);
                AppendStyle(excelColumnNames[1] + rowIndex.ToString(), "Total", newExcelRow, 2);
                AppendStyle(excelColumnNames[2] + rowIndex.ToString(), "Assigned", newExcelRow, 2);
                AppendStyle(excelColumnNames[3] + rowIndex.ToString(), "Available", newExcelRow, 2);
                AppendStyle(excelColumnNames[4] + rowIndex.ToString(), "Not available", newExcelRow, 2);
                AppendStyle(excelColumnNames[5] + rowIndex.ToString(), "Waiting for recycling", newExcelRow, 2);
                AppendStyle(excelColumnNames[6] + rowIndex.ToString(), "Recycled", newExcelRow, 2);

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

                    AppendStyle(excelColumnNames[0] + rowIndex.ToString(), item.Category.Name, newExcelRow, 3);
                    AppendStyle(excelColumnNames[1] + rowIndex.ToString(), item.Total.ToString(), newExcelRow, 3, true);
                    AppendStyle(excelColumnNames[2] + rowIndex.ToString(), item.Assigned.ToString(), newExcelRow, 3, true);
                    AppendStyle(excelColumnNames[3] + rowIndex.ToString(), item.Available.ToString(), newExcelRow, 3, true);
                    AppendStyle(excelColumnNames[4] + rowIndex.ToString(), item.NotAvailable.ToString(), newExcelRow, 3, true);
                    AppendStyle(excelColumnNames[5] + rowIndex.ToString(), item.WaitingForRecycling.ToString(), newExcelRow, 3, true);
                    AppendStyle(excelColumnNames[6] + rowIndex.ToString(), item.Recycled.ToString(), newExcelRow, 3, true);

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
