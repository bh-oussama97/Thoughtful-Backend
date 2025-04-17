using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.Commands;
using Thoughtful.Api.Features.Articles.DTO;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Articles.Handlers
{
    public class SetAuthorHandler : IRequestHandler<SetAuthor, Result<ArticleGetDto>>
    {
        protected IMapper _mapper;
        protected ThoughtfulDbContext _context;
        public SetAuthorHandler(ThoughtfulDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<Result<ArticleGetDto>> Handle(SetAuthor request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == request.Id);
            var author = await _context.Authors.FindAsync(request.AuthorId);

            if (article == null || author == null)
            {
                return Result<ArticleGetDto>.Failure(new Error($"Article {request.Id} or Author {request.AuthorId} is null ", "NullObject"));
            }

            article.Author = author;
            await _context.SaveChangesAsync();
            var result = new Result<ArticleGetDto>();
            result.IsSuccess = true;
            result.Body = _mapper.Map<ArticleGetDto>(article);
            return result;
        }

    }
}
