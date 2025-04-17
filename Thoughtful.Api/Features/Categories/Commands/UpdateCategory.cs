using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Categories.Dto;

namespace Thoughtful.Api.Features.Categories.Commands
{
    public class UpdateCategory : IRequest<Result<CategoryGetDto>>
    {
        public int Id { get; set; }
        public CategoryDto Category { get; set; }
    }
}
