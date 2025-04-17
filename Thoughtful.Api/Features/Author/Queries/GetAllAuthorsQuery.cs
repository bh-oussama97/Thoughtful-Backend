using Thoughtful.Api.Common;
using Thoughtful.Api.Features.AuthorFeature;

namespace Thoughtful.Api.Features.Author.Queries
{
    public class GetAllAuthorsQuery : IRequest<Result<List<AuthorGetDto>>>
    {
    }
}
