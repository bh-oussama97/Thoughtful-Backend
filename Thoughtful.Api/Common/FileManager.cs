using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.StaticFiles;

namespace Thoughtful.Api.Common
{
    public class FileManager
    {
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

        public static string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(path, out var contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
    }
}
