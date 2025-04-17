

using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.DTO;
using Thoughtful.Api.Features.Articles.Queries;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Articles.Handlers
{
    public class GetAllByAuthorHandler : IRequestHandler<GetAllArticlesByAuthorQuery, Result<List<ArticleGetDto>>>
    {
        private readonly ThoughtfulDbContext _context;
        private readonly IMapper _mapper;
        public GetAllByAuthorHandler(ThoughtfulDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<Result<List<ArticleGetDto>>> Handle(GetAllArticlesByAuthorQuery request, CancellationToken cancellationToken)
        {
            var articlesByAuthors = await _context.Articles
                .Include(x => x.Author)
                .Include(x => x.Category)
                .Where(el => el.AuthorId.Equals(request.AuthorId)).ToListAsync();

            if (articlesByAuthors is null)
            {
                return await Task.FromResult(Result<List<ArticleGetDto>>.Failure(new Error("Articles list is null")));
            }
            List<ArticleGetDto> articlesListMapped = articlesByAuthors.Select(el => this._mapper.Map<ArticleGetDto>(el)).ToList();
            return await Task.FromResult(Result<List<ArticleGetDto>>.Success(articlesListMapped));
        }
    }
}
