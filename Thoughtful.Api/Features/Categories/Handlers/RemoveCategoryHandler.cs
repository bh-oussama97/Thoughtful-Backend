using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Categories.Commands;
using Thoughtful.Dal;


namespace Thoughtful.Api.Features.Categories.Handlers
{
    public class RemoveCategoryHandler : IRequestHandler<RemoveCategory, Result<Unit>>
    {
        private readonly ThoughtfulDbContext _ctx;

        public RemoveCategoryHandler(ThoughtfulDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Result<Unit>> Handle(RemoveCategory request, CancellationToken cancellationToken)
        {
            var categoryById = await _ctx.Categories.FirstOrDefaultAsync(x => x.Id == request.CategoryId);

            if (categoryById == null)
            {
                return Result<Unit>.Failure(new Error("Category Not Found", "InvalidCategoryError"));
            }

            _ctx.Categories.Remove(categoryById);
            int result = await _ctx.SaveChangesAsync(cancellationToken);

            if (result == 0)
            {
                return Result<Unit>.Failure(new Error("Failed to delete category", "DatabaseError"));
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }

}
