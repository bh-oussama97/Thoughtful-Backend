using Thoughtful.Api.Common;

namespace Thoughtful.Api.Features.Author.Commands
{
    public class DownloadBlogFileCommand : IRequest<Result<byte[]>>
    {
        public string Filename { get; set; }
    }
}
