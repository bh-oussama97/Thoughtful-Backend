using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.DTO;
using Thoughtful.Api.Features.Articles.Queries;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Articles.Handlers
{
    public class GetAllArticlesHandler : IRequestHandler<GetAllArticlesQuery, Result<List<ArticleGetDto>>>
    {
        private readonly ThoughtfulDbContext _context;
        private readonly IMapper _mapper;
        public GetAllArticlesHandler(ThoughtfulDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<Result<List<ArticleGetDto>>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken)
        {
            Result<List<ArticleGetDto>> result = new Result<List<ArticleGetDto>>();
            var articlesList = await _context.Articles.
                Include(a => a.Author).
                Include(a =>
                a.Category).ToListAsync();
            List<ArticleGetDto> articlesListmMapped = articlesList.Select(x => _mapper.Map<ArticleGetDto>(x)).ToList();
            result.Body = articlesListmMapped;
            result.IsSuccess = true;
            return result;
        }
    }
}
