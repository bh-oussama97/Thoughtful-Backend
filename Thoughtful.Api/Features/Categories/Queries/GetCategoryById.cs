using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Categories.Dto;

namespace Thoughtful.Api.Features.Categories.Queries
{
    public class GetCategoryById : IRequest<Result<CategoryGetDto>>
    {
        public int CategoryId { get; set; }
    }
}
