using Thoughtful.Api.Common;

namespace Thoughtful.Api.Features.Author.Commands
{
    public class DeleteAuthor : IRequest<Result<string>>
    {
        public int AuthorId { get; init; }
    }
}
