using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.DTO;

namespace Thoughtful.Api.Features.Blogs.Queries
{
    public class GetBlogById : IRequest<Result<BlogGetDTO>>
    {
        public int BlogId { get; set; }
    }
}
