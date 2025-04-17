using Thoughtful.Api.Common;

namespace Thoughtful.Api.Features.Articles.Commands
{
    public class DeleteArticle : IRequest<Result<string>>
    {
        public int ArticleId { get; set; }

    }
}
