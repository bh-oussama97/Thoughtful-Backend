using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Author.Commands;

namespace Thoughtful.Api.Features.Author.Handlers
{
    public class DownloadBlogFileHandler : IRequestHandler<DownloadBlogFileCommand, Result<byte[]>>
    {
        public Task<Result<byte[]>> Handle(DownloadBlogFileCommand request, CancellationToken cancellationToken)
        {
            string _resroucesPath = AppSettings.UploadFilePath;
            string filePath = Path.Combine(_resroucesPath, request.Filename);


            return Task.FromResult(Result<byte[]>.Success(File.ReadAllBytes(filePath)));
        }
    }
}
