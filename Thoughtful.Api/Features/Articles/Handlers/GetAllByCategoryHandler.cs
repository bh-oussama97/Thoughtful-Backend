using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.DTO;
using Thoughtful.Api.Features.Articles.Queries;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Articles.Handlers
{
    public class GetAllByCategoryHandler : IRequestHandler<GetAllByCategory, Result<List<ArticleGetDto>>>
    {
        private readonly ThoughtfulDbContext _context;
        private readonly IMapper _mapper;
        public GetAllByCategoryHandler(ThoughtfulDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<Result<List<ArticleGetDto>>> Handle(GetAllByCategory request, CancellationToken cancellationToken)
        {
            var articlesByCategory = await _context.Articles
                .Where(a => a.Category.Id == request.CategoryId)
                .Select(b => new ArticleGetDto(b)).ToListAsync(cancellationToken);
            return Result<List<ArticleGetDto>>.Success(articlesByCategory);
        }
    }
}
