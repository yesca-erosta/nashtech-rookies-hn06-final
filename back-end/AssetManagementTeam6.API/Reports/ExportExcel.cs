using DocumentFormat.OpenXml.Spreadsheet;

namespace AssetManagementTeam6.API.Reports
{
    public partial class ExportExcel
    {
        private static string ConvertToExcelDate(DateTime d)
        {
            //Excel reproduced a bug from Lotus for compatibility reasons, 29/02/1900 didn't actually exist.
            if (d.Ticks < 599317056000000000)
                return ((int)d.ToOADate() - 1).ToString();
            return ((int)d.ToOADate()).ToString();
        }

        private static string GetExcelColumnName(int columnIndex)
        {
            if (columnIndex < 26)
                return ((char)('A' + columnIndex)).ToString();

            char firstChar = (char)('A' + (columnIndex / 26) - 1);
            char secondChar = (char)('A' + (columnIndex % 26));
            return string.Format("{0}{1}", firstChar, secondChar);
        }

        private static void AppendStyle(string cellReference, string cellStringValue, Row excelRow,
            uint cell_Formats_Index = 0, bool isNumberic = false)
        {
            Cell cell = new()
            {
                CellReference = cellReference,
                DataType = isNumberic ? CellValues.Number : CellValues.String,
                StyleIndex = cell_Formats_Index
            };
            CellValue cellValue = new();
            cellValue.Text = cellStringValue;
            cell.Append(cellValue);
            excelRow.Append(cell);
        }
    }
}
