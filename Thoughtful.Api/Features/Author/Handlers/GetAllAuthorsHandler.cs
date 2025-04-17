using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Author.Queries;
using Thoughtful.Api.Features.AuthorFeature;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Author.Handlers
{
    public class GetAllAuthorsHandler : IRequestHandler<GetAllAuthorsQuery, Result<List<AuthorGetDto>>>
    {
        private readonly ThoughtfulDbContext _ctx;
        private readonly IMapper _mapper;
        public GetAllAuthorsHandler(ThoughtfulDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<Result<List<AuthorGetDto>>> Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
        {
            var authors = await _ctx.Authors.ToListAsync();
            var result = new Result<List<AuthorGetDto>>();
            result.IsSuccess = true;
            result.Body = _mapper.Map<List<AuthorGetDto>>(authors);
            return result;
        }
    }
}
