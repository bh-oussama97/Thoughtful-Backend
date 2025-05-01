using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Author.Queries;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Author.Handlers
{
    public class ExportXLSHandler : IRequestHandler<ExportXLSQuery, Result<byte[]>>
    {
        protected ThoughtfulDbContext context;
        public ExportXLSHandler(ThoughtfulDbContext _context)
        {
            context = _context;
        }
        public async Task<Result<byte[]>> Handle(ExportXLSQuery request, CancellationToken cancellationToken)
        {
            var blogsData = context.Blogs.ToList();

            return Result<byte[]>.Success(SaveToExcelFile(blogsData));
        }

        public byte[] SaveToExcelFile(List<Domain.Model.Blog> blogsData)
        {

            string _resroucesPath = AppSettings.UploadFilePath;
            string filePath = Path.Combine(_resroucesPath, $"Blogs-Data-{DateTime.Now:yyyy-MM-dd}.xlsx");

            using (SpreadsheetDocument document = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorkbookStylesPart stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                stylesPart.Stylesheet = FileManager.CreateStylesheet();
                stylesPart.Stylesheet.Save();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                SheetData sheetData = new SheetData();

                Columns columns = new Columns();
                columns.Append(
                    FileManager.CreateColumn(1, 1, 25),
                    FileManager.CreateColumn(2, 2, 50),
                    FileManager.CreateColumn(3, 3, 20)
                );

                worksheetPart.Worksheet = new Worksheet();
                worksheetPart.Worksheet.Append(columns);
                worksheetPart.Worksheet.Append(sheetData);

                Sheets sheets = document.WorkbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet()
                {
                    Id = document.WorkbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Blogs"
                };
                sheets.Append(sheet);

                Row headerRow = new Row();
                headerRow.Append(
                    FileManager.CreateStyledCell("Name", 1),
                    FileManager.CreateStyledCell("Description", 1),
                    FileManager.CreateStyledCell("Created Date", 1)
                );
                sheetData.Append(headerRow);

                foreach (var item in blogsData)
                {
                    Row dataRow = new Row();
                    dataRow.Append(
                        FileManager.CreateStyledCell(item.Name, 3),
                        FileManager.CreateStyledCell(item.Description, 3),
                        FileManager.CreateStyledCell(item.CreatedDate.ToString("yyyy-MM-dd"), 3)
                    );
                    sheetData.Append(dataRow);
                }

                workbookPart.Workbook.Save();
            }

            return File.ReadAllBytes(filePath);


        }
    }
}
