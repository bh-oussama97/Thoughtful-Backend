using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.DTO;

namespace Thoughtful.Api.Features.Blogs.Commands
{
    public class CreateBlog : IRequest<Result<BlogGetDTO>>
    {
        public BlogDTO NewBlog { get; set; }
    }
}
