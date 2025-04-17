using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.DTO;

namespace Thoughtful.Api.Features.Blogs.Commands
{
    public class UpdateBlogInfo : IRequest<Result<BlogGetDTO>>
    {
        public int BlogId { get; init; }
        public BlogDTO Blog { get; set; }


    }
}
