using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.Commands;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Blogs.Handlers
{
    public class AddContributorHandler : IRequestHandler<AddContributor, Result<string>>
    {
        private readonly ThoughtfulDbContext _ctx;
        public AddContributorHandler(ThoughtfulDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Result<string>> Handle(AddContributor request, CancellationToken cancellationToken)
        {
            Result<string> result = new Result<string>();

            var blog = await _ctx.Blogs.Include(b => b.Contributors)
                .FirstOrDefaultAsync(b => b.Id == request.BlogId);

            var contributor = await _ctx.Authors.FirstOrDefaultAsync(a => a.Id == request.ContributorId);

            if (blog == null || contributor == null)
            {
                result.IsSuccess = false;
                result.Body = "Invalid blog or contributor ID.";
                return result;
            }

            _ctx.Authors.Attach(contributor);

            if (!blog.Contributors.Any(c => c.Id == contributor.Id))
            {
                blog.Contributors.Add(contributor);
                await _ctx.SaveChangesAsync();

                result.IsSuccess = true;
                result.Body = $"Contributor with Id {request.ContributorId} has been added successfully to the blog {request.BlogId}";
                return result;
            }

            result.IsSuccess = false;
            result.Body = $"Contributor with Id {request.ContributorId} is already added to the blog {request.BlogId}";
            return result;
        }
    }
}
