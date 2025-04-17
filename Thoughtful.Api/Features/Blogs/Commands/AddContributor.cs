using Thoughtful.Api.Common;

namespace Thoughtful.Api.Features.Blogs.Commands
{
    public class AddContributor : IRequest<Result<string>>
    {
        public int BlogId { get; init; }
        public int ContributorId { get; init; }
    }
}
