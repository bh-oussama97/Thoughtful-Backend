using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Author.Commands;
using Thoughtful.Api.Features.AuthorFeature;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Author.Handlers
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthor, Result<AuthorGetDto>>
    {
        private readonly ThoughtfulDbContext _ctx;
        private readonly IMapper _mapper;
        public UpdateAuthorHandler(ThoughtfulDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            this._mapper = mapper;

        }

        async Task<Result<AuthorGetDto>> IRequestHandler<UpdateAuthor, Result<AuthorGetDto>>.Handle(UpdateAuthor request, CancellationToken cancellationToken)
        {
            var author = await _ctx.Authors.FirstOrDefaultAsync(a => a.Id == request.AuthorId);
            if (author is not null)
            {
                author.FirstName = request.AuthorDto.FirstName;
                author.LastName = request.AuthorDto.LastName;
                author.Bio = request.AuthorDto.Bio;
                author.DateOfBirth = request.AuthorDto.DateOfBirth;
            }

            int result = await _ctx.SaveChangesAsync();

            if (result > 0)
            {
                return await Task.FromResult(Result<AuthorGetDto>.Success(_mapper.Map<AuthorGetDto>(author)));
            }
            return await Task.FromResult(Result<AuthorGetDto>.Failure(new Error("Error when updating author", "ErrorUpdating")));
        }
    }
}
