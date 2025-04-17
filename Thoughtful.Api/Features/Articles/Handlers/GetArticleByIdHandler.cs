using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.DTO;
using Thoughtful.Api.Features.Articles.Queries;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Articles.Handlers
{
    public class GetArticleByIdHandler : IRequestHandler<GetArticleByIdQuery, Result<ArticleGetDto>>
    {
        protected ThoughtfulDbContext _context;
        protected IMapper _mapper;
        public GetArticleByIdHandler(ThoughtfulDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<Result<ArticleGetDto>> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles
               .Include(a => a.Author)
               .Include(a => a.Category)
               .FirstOrDefaultAsync(a => a.Id == request.Id);

            if (article == null)
            {
                return Result<ArticleGetDto>.Failure(new Error("article not found", "NotFound"));
            }

            return Result<ArticleGetDto>.Success(_mapper.Map<ArticleGetDto>(article));
        }
    }
}
