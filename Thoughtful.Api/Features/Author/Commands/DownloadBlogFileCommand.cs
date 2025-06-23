using Microsoft.AspNetCore.Mvc;
using Thoughtful.Api.Common;

namespace Thoughtful.Api.Features.Author.Commands
{
    public class DownloadBlogFileCommand : IRequest<IActionResult>
    {
        public string Filename { get; set; }
    }
}
