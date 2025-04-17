using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.DTO;

namespace Thoughtful.Api.Features.Blogs.Queries
{
    public class GetAllBlogs : IRequest<Result<List<BlogGetDTO>>>
    {
    }
}
