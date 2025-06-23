using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Author.Commands;

namespace Thoughtful.Api.Features.Author.Handlers
{
    public class DownloadBlogFileHandler : IRequestHandler<DownloadBlogFileCommand, IActionResult>
    {
        public DownloadBlogFileHandler()
        {
        }

        public async Task<IActionResult> Handle(DownloadBlogFileCommand request, CancellationToken cancellationToken)
        {
            string _uploadRoot = AppSettings.UploadFilePath;
            string filePath = Path.Combine(_uploadRoot, request.Filename);
            Console.WriteLine($"filePath {filePath}");

            if (!File.Exists(filePath)) 
            {
                return new NotFoundObjectResult("File not found");
            }

            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return new FileStreamResult(memory, GetContentType(filePath))
            {
                FileDownloadName = Path.GetFileName(filePath)
            };
        }
        private string GetContentType(string path)
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
