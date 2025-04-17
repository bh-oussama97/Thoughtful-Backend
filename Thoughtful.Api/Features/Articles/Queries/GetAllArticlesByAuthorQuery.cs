using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.DTO;

namespace Thoughtful.Api.Features.Articles.Queries
{
    public class GetAllArticlesByAuthorQuery : IRequest<Result<List<ArticleGetDto>>>
    {
        public int AuthorId { get; set; }
    }
}
