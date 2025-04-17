using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.DTO;

namespace Thoughtful.Api.Features.Articles.Commands
{
    public class UpdateArticleInfos : IRequest<Result<ArticleGetDto>>
    {
        public int ArticleId { get; set; }
        public ArticleDTO ArticleDTO { get; set; }
    }
}
