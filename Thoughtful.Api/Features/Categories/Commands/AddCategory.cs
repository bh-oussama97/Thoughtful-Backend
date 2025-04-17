using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Categories.Dto;

namespace Thoughtful.Api.Features.Categories.Commands
{
    public class AddCategory : IRequest<Result<CategoryGetDto>>
    {
        public CategoryDto? Category { get; set; }
    }
}
