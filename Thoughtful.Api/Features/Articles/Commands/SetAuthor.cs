using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.DTO;

namespace Thoughtful.Api.Features.Articles.Commands
{
    public class SetAuthor : IRequest<Result<ArticleGetDto>>
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
    }
}
