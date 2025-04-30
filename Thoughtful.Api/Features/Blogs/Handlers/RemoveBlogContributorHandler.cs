using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.Commands;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Blogs.Handlers
{
    public class RemoveBlogContributorHandler : IRequestHandler<RemoveBlogContributor, Result<string>>
    {
        private readonly ThoughtfulDbContext _ctx;
        public RemoveBlogContributorHandler(ThoughtfulDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Result<string>> Handle(RemoveBlogContributor request, CancellationToken cancellationToken)
        {
            Result<string> resultDeletion = new Result<string>();

            //var blog = await _ctx.Blogs.Include(b => b.Contributors)
            //    .FirstOrDefaultAsync(b => b.Id == request.BlogId);

            //var author = await _ctx.Authors.FirstOrDefaultAsync(a => a.Id == request.ContributorId);

            //if (blog != null && author != null)
            //    blog.Contributors.Remove(author);

            //int result = await _ctx.SaveChangesAsync();
            //if (result > 0)
            //{
            //    resultDeletion.IsSuccess = true;
            //    resultDeletion.Body = $"Contributor with Id {request.ContributorId} has been successfully removed from the blog {request.BlogId}";
            //    return await Task.FromResult(resultDeletion);
            //}

            //resultDeletion.IsSuccess = false;
            //resultDeletion.Body = "Error while removing contributor";
            return await Task.FromResult(resultDeletion);
        }
    }
}
