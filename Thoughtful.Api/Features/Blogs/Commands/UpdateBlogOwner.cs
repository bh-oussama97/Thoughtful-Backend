using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.DTO;

namespace Thoughtful.Api.Features.Blogs.Commands
{
    public class UpdateBlogOwner : IRequest<Result<BlogGetDTO>>
    {
        public int BlogId { get; init; }
        public int OnwerId { get; init; }
    }
}
