using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.DTO;

namespace Thoughtful.Api.Features.Articles.Queries
{
    public class GetAllByCategory : IRequest<Result<List<ArticleGetDto>>>
    {
        public int CategoryId { get; set; }
    }
}
