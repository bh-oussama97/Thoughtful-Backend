using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.DTO;

namespace Thoughtful.Api.Features.Articles.Commands
{
    public class CreateArticle : IRequest<Result<ArticleGetDto>>
    {
        public ArticleDTO Article { get; set; }

    }
}
