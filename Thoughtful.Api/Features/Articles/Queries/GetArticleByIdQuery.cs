using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.DTO;

namespace Thoughtful.Api.Features.Articles.Queries
{
    public class GetArticleByIdQuery : IRequest<Result<ArticleGetDto>>
    {
        public int Id { get; set; }
    }
}
