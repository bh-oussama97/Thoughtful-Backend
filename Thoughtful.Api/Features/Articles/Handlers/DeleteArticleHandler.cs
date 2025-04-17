using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.Commands;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Articles.Handlers
{
    public class DeleteArticleHandler : IRequestHandler<DeleteArticle, Result<string>>
    {
        private readonly ThoughtfulDbContext _context;
        public DeleteArticleHandler(ThoughtfulDbContext context)
        {
            this._context = context;
        }
        public async Task<Result<string>> Handle(DeleteArticle request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.FindAsync(request.ArticleId);
            Result<string> resultDeletion = new Result<string>();

            if (article == null)
            {
                return Result<string>.Failure(new Error("Article Not Found", "InvalidArticleError"));
            }
            _context.Articles.Remove(article);
            int result = await _context.SaveChangesAsync(cancellationToken);


            if (result > 0)
            {
                resultDeletion.IsSuccess = true;
                resultDeletion.Body = $"item with Id {request.ArticleId} has been successfully deleted";
                return await Task.FromResult(resultDeletion);
            }

            resultDeletion.IsSuccess = false;
            resultDeletion.Body = "Error when deleting article";
            return await Task.FromResult(resultDeletion);

            //if (result == 0)
            //{
            //    return Result<string>.Failure(new Error("Failed to delete article", "DatabaseError"));
            //}

            //return Result<string>.Success(Unit.Value);
        }
    }
}
