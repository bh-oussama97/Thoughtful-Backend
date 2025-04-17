using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.Commands;
using Thoughtful.Api.Features.Articles.DTO;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Articles.Handlers
{
    public class AddCategoryToArticleHandler : IRequestHandler<AddCategoryToArticle, Result<ArticleGetDto>>
    {
        protected ThoughtfulDbContext _context;
        protected IMapper _mapper;
        public AddCategoryToArticleHandler(ThoughtfulDbContext context, IMapper mapper) { _context = context; _mapper = mapper; }
        public async Task<Result<ArticleGetDto>> Handle(AddCategoryToArticle request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(a => a.Id == request.Id);
            var category = await _context.Categories.FindAsync(request.CategoryId);

            if (article == null || category == null)
            {
                return Result<ArticleGetDto>.Failure(new Error($"Article {request.Id} or Category {request.CategoryId} is null ", "NullObject"));
            }

            article.Category = category;
            await _context.SaveChangesAsync();
            var result = new Result<ArticleGetDto>();
            result.IsSuccess = true;
            result.Body = _mapper.Map<ArticleGetDto>(article);
            return result;
        }
    }
}
