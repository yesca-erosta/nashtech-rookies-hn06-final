using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;

namespace AssetManagementTeam6.API.Reports
{
    public partial class CustomExportExcel
    {
        private Stylesheet GenerateStyleSheetForSampleReport()
        {
            Stylesheet ss = new();

            ss.NumberingFormats = new NumberingFormats();

            NumberingFormat numberingFormat1 = new() { NumberFormatId = (UInt32Value)164U, FormatCode = "[$-409]d\\-mmm\\-yyyy;@" };

            ss.NumberingFormats.Append(numberingFormat1);

            ss.Append(new Fonts(
                    // Index 0 – The default font. Calibri Black 11pt Normal
                    new Font(
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue("000000") },
                        new FontName() { Val = "Calibri" }),
                    // Index 1 – The default font. Calibri Black 11pt Bold
                    new Font(
                        new Bold(),
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue("000000") },
                        new FontName() { Val = "Calibri" }),
                    // Index 2 – The default font. Calibri Red 11pt Bold
                    new Font(
                        new Bold(),
                        new FontSize() { Val = 11 },
                        new Color() { Rgb = new HexBinaryValue("ff0000") },
                        new FontName() { Val = "Calibri" })
                ));

            ss.Append(new Fills(
                    // Index 0 – The default fill - nofill. - default, do not remove
                    new Fill(
                        new PatternFill() { PatternType = PatternValues.None }),
                    // Index 1 – The default fill of gray 125 (required) - default, do not remove
                    new Fill(
                        new PatternFill() { PatternType = PatternValues.Gray125 }),
                    // Index 2 – The yellow fill - solid.
                    new Fill(
                        new PatternFill(
                            new ForegroundColor() { Rgb = new HexBinaryValue("b4c6e7") }
                        )
                        { PatternType = PatternValues.Solid }),
                    // Index 3 – The d2c29d - solid.
                    new Fill(
                        new PatternFill(
                            new ForegroundColor() { Rgb = new HexBinaryValue("d2c29d") }
                        )
                        { PatternType = PatternValues.Solid })
                )); 

            ss.Append(new Borders(
                    // Index 0 – The default border. - do not remove
                    new Border(
                        new LeftBorder(),
                        new RightBorder(),
                        new TopBorder(),
                        new BottomBorder(),
                        new DiagonalBorder()),
                    // Index 1 – Applies a Left, Right, Top, Bottom border to a cell
                    new Border(
                        new LeftBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new RightBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new TopBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new BottomBorder(
                            new Color() { Auto = true }
                        )
                        { Style = BorderStyleValues.Thin },
                        new DiagonalBorder())
                //More border here...
                ));

            ss.Append(new CellFormats(
                   //Index 0 – no boder, no bold. - default, do not remove
                   new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 },
                   //Index 1 – no boder, bold - default, do not remove
                   new CellFormat() { FontId = 1, FillId = 0, BorderId = 0 },

                   //Index 2 - Bold, no fill, all border, center align horizol & vertical, wrap text
                   new CellFormat(new Alignment()
                   {
                       Vertical = VerticalAlignmentValues.Center,
                       Horizontal = HorizontalAlignmentValues.Center,
                       WrapText = true
                   })
                   { FontId = 1, FillId = 0, BorderId = 1, ApplyAlignment = true, ApplyBorder = true },

                    //Index 3 - Normal, all border, center align vertical
                    new CellFormat(new Alignment()
                    {
                        Vertical = VerticalAlignmentValues.Center
                    })
                    { FontId = 0, FillId = 0, BorderId = 1, ApplyAlignment = true, ApplyBorder = true },

                   // Index 4 – Normal, all border, center align vertical, numbering format 164
                   new CellFormat(new Alignment()
                   {
                       Vertical = VerticalAlignmentValues.Center
                   })
                   { FontId = 0, FillId = 0, BorderId = 1, NumberFormatId = 164U, ApplyNumberFormat = true, ApplyAlignment = true, ApplyBorder = true },

                   // Index 5 – Normal, all border, center align vertical, numbering format 4 : 8 => 8.00
                   new CellFormat(new Alignment()
                   {
                       Vertical = VerticalAlignmentValues.Center
                   })
                   { FontId = 0, FillId = 0, BorderId = 1, NumberFormatId = 4U, ApplyNumberFormat = true, ApplyAlignment = true, ApplyBorder = true },

                   // Index 6 – Remark: Normal, indent, all border, center align vertical
                   new CellFormat(new Alignment()
                   {
                       Vertical = VerticalAlignmentValues.Center,
                       Horizontal = HorizontalAlignmentValues.Left,
                       WrapText = true,
                       Indent = 1U
                   })
                   { FontId = 0, FillId = 0, BorderId = 1, ApplyAlignment = true, ApplyBorder = true },

                   // Index 7 – Pass: Normal, indent, yellow fill, all border, center align vertical
                   new CellFormat(new Alignment()
                   {
                       Vertical = VerticalAlignmentValues.Center,
                       Horizontal = HorizontalAlignmentValues.Left,
                       WrapText = true,
                       Indent = 1U
                   })
                   { FontId = 0, FillId = 2, BorderId = 1, ApplyAlignment = true, ApplyFill = true, ApplyBorder = true },

                   // Index 8 – Failed: Normal, indent, brown fill, all border, center align vertical
                   new CellFormat(new Alignment()
                   {
                       Vertical = VerticalAlignmentValues.Center,
                       Horizontal = HorizontalAlignmentValues.Left,
                       WrapText = true,
                       Indent = 1U
                   })
                   { FontId = 0, FillId = 3, BorderId = 1, ApplyAlignment = true, ApplyFill = true, ApplyBorder = true },

                   //Index 9 - Bold, red, no fill, all border, center align horizol & vertical, wrap text
                   new CellFormat(new Alignment()
                   {
                       Vertical = VerticalAlignmentValues.Center,
                       Horizontal = HorizontalAlignmentValues.Center,
                       WrapText = true
                   })
                   { FontId = 2, FillId = 0, BorderId = 1, ApplyAlignment = true, ApplyBorder = true }

               ));
            ss.CellFormats!.Count = UInt32Value.FromUInt32((uint)ss.CellFormats.ChildElements.Count);
            return ss;
        }
    }
}
