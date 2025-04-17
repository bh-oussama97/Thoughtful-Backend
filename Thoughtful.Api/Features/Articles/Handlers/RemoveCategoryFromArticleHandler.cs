using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.Commands;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Articles.Handlers
{
    public class RemoveCategoryFromArticleHandler : IRequestHandler<RemoveCategoryFromArticle, Result<string>>
    {
        private readonly ThoughtfulDbContext _context;

        public RemoveCategoryFromArticleHandler(ThoughtfulDbContext context)
        {
            _context = context;
        }
        public async Task<Result<string>> Handle(RemoveCategoryFromArticle request, CancellationToken cancellationToken)
        {
            Result<string> deletionResult = new Result<string>();
            var article = await _context.Articles.Include(a => a.Category).FirstOrDefaultAsync(a => a.Id == request.ArticleId);
            var category = await _context.Categories.FindAsync(request.CategoryId);

            if (article == null || category == null)
            {
                deletionResult.IsSuccess = false;
                deletionResult.Body = "article or category is null";
            }
            else
            {
                article.Category = null;
                int result = await _context.SaveChangesAsync();
                if (result != 0)
                {
                    deletionResult.IsSuccess = true;
                    deletionResult.Body = $"Category with Id {request.CategoryId} is removed from the article {request.ArticleId}";
                }
            }


            return await Task.FromResult(deletionResult);

        }
    }
}
