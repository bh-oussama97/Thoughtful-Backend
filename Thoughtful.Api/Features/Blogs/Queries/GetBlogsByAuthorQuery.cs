using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.DTO;

namespace Thoughtful.Api.Features.Blogs.Queries
{
    public class GetBlogsByAuthorQuery : IRequest<Result<List<BlogGetDTO>>>
    {
        public int AuthorId { get; set; }
    }
}
