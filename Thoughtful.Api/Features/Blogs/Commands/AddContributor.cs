using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.DTO;

namespace Thoughtful.Api.Features.Blogs.Commands
{
    public class AddContributor : IRequest<Result<string>>
    {
        public ContributionDTO Contribution { get; set; }
    }
}
