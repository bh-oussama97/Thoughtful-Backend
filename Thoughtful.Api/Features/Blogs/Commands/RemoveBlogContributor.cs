using Thoughtful.Api.Common;

namespace Thoughtful.Api.Features.Blogs.Commands
{
    public class RemoveBlogContributor : IRequest<Result<string>>
    {
        public int BlogId { get; init; }
        public int ContributorId { get; init; }
    }
}
