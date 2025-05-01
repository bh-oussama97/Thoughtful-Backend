using DocumentFormat.OpenXml.Spreadsheet;

namespace Thoughtful.Api.Common
{
    public class FileManager
    {

        public static byte[] ConvertBase6(string filePath, string filename)
        {
            string imagePath = Path.Combine(filePath, filename);
            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
            return imageBytes;
        }

        public static Row CreateKeyValueRow(string key, string value)
        {
            Row row = new Row();
            row.Append(CreateCell(key), CreateCell(value));
            return row;
        }

        public static Row CreateMergedRow(string text, int mergeColumns, bool bold = false, int fontSize = 14)
        {
            Row row = new Row();
            Cell cell = CreateCell(text);
            row.Append(cell);

            for (int i = 1; i < mergeColumns; i++)
            {
                row.Append(new Cell());
            }

            return row;
        }

        public static Cell CreateCell(string value)
        {
            return new Cell()
            {
                DataType = CellValues.String,
                CellValue = new CellValue(value ?? string.Empty)
            };
        }

        public static Cell CreateStyledCell(string text, uint styleIndex)
        {
            return new Cell
            {
                DataType = CellValues.String,
                CellValue = new CellValue(text ?? ""),
                StyleIndex = styleIndex
            };
        }

        public static Stylesheet CreateStylesheet()
        {
            return new Stylesheet(
                new Fonts(
                    new Font(), // Index 0 - default
                    new Font(new Bold(), new FontSize() { Val = 12 }, new Color() { Rgb = "000000" }) // Index 1 - bold black
                ),
                new Fills(
                    new Fill(new PatternFill() { PatternType = PatternValues.None }), // Index 0
                    new Fill(new PatternFill() { PatternType = PatternValues.Gray125 }), // Index 1
                    new Fill(new PatternFill(new ForegroundColor { Rgb = "8EA9DB" }) { PatternType = PatternValues.Solid }), // Index 2 - header
                    new Fill(new PatternFill(new ForegroundColor { Rgb = "FFFFFF" }) { PatternType = PatternValues.Solid })  // Index 3 - white
                ),
                new Borders(
                    new Border(), // Index 0
                    new Border(   // Index 1 - thin borders
                        new LeftBorder { Style = BorderStyleValues.Thin },
                        new RightBorder { Style = BorderStyleValues.Thin },
                        new TopBorder { Style = BorderStyleValues.Thin },
                        new BottomBorder { Style = BorderStyleValues.Thin }
                    )
                ),
                new CellFormats(
                    new CellFormat(), // Index 0 - default
                    new CellFormat { FontId = 1, FillId = 2, BorderId = 1, ApplyFont = true, ApplyFill = true, ApplyBorder = true }, // Index 1 - header
                    new CellFormat { FontId = 0, FillId = 3, BorderId = 1, ApplyFill = true, ApplyBorder = true }, // Index 2 - optional
                    new CellFormat { FontId = 0, FillId = 3, BorderId = 1, ApplyFill = true, ApplyBorder = true }  // Index 3 - content
                )
            );
        }


        public static Column CreateColumn(uint min, uint max, double width)
        {
            return new Column
            {
                Min = min,
                Max = max,
                Width = width,
                CustomWidth = true
            };
        }


    }
}
