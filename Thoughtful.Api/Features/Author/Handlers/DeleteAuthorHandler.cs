using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Author.Commands;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Author.Handlers
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthor, Result<string>>
    {
        private readonly ThoughtfulDbContext _ctx;
        public DeleteAuthorHandler(ThoughtfulDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Result<string>> Handle(DeleteAuthor request, CancellationToken cancellationToken)
        {
            var author = await _ctx.Authors.FirstOrDefaultAsync(a => a.Id == request.AuthorId);
            Result<string> resultDeletion = new Result<string>();

            if (author != null)
                _ctx.Authors.Remove(author);

            int result = await _ctx.SaveChangesAsync();

            if (result > 0)
            {
                resultDeletion.IsSuccess = true;
                resultDeletion.Body = $"item with Id {request.AuthorId} has been successfully deleted";
                return await Task.FromResult(resultDeletion);
            }

            resultDeletion.IsSuccess = false;
            resultDeletion.Body = "Error when deleting author";
            return await Task.FromResult(resultDeletion);
        }
    }
}
