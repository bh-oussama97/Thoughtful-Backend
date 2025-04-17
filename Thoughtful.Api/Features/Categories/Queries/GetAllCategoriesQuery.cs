using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Categories.Dto;

namespace Thoughtful.Api.Features.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<Result<List<CategoryGetDto>>>
    {
    }
}
