using DocumentFormat.OpenXml.Spreadsheet;

namespace AssetManagementTeam6.API.Reports
{
    public partial class CustomExportExcel
    {
        public Columns GenerateColumnsForSampleReport()
        {
            Columns columnSet = new();

            Column cc;
            cc = new Column() { Min = 1U, Max = 1U, Width = 20D, CustomWidth = true };
            columnSet.Append(cc);
            cc = new Column() { Min = 2U, Max = 2U, Width = 15D, CustomWidth = true };
            columnSet.Append(cc);
            cc = new Column() { Min = 3U, Max = 3U, Width = 15D, CustomWidth = true };
            columnSet.Append(cc);
            cc = new Column() { Min = 4U, Max = 4U, Width = 20D, CustomWidth = true };
            columnSet.Append(cc);
            cc = new Column() { Min = 5U, Max = 5U, Width = 20D, CustomWidth = true };
            columnSet.Append(cc);
            cc = new Column() { Min = 6U, Max = 6U, Width = 25D, CustomWidth = true };
            columnSet.Append(cc);
            cc = new Column() { Min = 7U, Max = 7U, Width = 15D, CustomWidth = true };
            columnSet.Append(cc);

            return columnSet;
        }

        //TODO: Create other GenerateColumn Function Here
    }
}
