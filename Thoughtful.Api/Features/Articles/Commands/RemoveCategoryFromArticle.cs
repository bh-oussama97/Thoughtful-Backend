using Thoughtful.Api.Common;

namespace Thoughtful.Api.Features.Articles.Commands
{
    public class RemoveCategoryFromArticle : IRequest<Result<string>>
    {
        public int CategoryId { get; set; }
        public int ArticleId { get; set; }
    }
}
