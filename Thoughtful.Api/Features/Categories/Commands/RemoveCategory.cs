using Thoughtful.Api.Common;

namespace Thoughtful.Api.Features.Categories.Commands
{
    public class RemoveCategory : IRequest<Result<Unit>>
    {
        public int CategoryId { get; set; }
    }
}
