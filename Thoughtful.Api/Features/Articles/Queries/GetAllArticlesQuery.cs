using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.DTO;

namespace Thoughtful.Api.Features.Articles.Queries
{
    public class GetAllArticlesQuery : IRequest<Result<List<ArticleGetDto>>>
    {
    }
}
