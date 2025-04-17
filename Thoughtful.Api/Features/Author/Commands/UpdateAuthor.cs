using Thoughtful.Api.Common;
using Thoughtful.Api.Features.AuthorFeature;

namespace Thoughtful.Api.Features.Author.Commands
{
    public class UpdateAuthor : IRequest<Result<AuthorGetDto>>
    {
        public int AuthorId { get; init; }
        public AuthorDto AuthorDto { get; set; }
    }
}
