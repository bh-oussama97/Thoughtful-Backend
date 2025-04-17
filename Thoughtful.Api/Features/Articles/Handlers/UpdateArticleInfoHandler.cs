using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.Commands;
using Thoughtful.Api.Features.Articles.DTO;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Articles.Handlers
{
    public class UpdateArticleInfoHandler : IRequestHandler<UpdateArticleInfos, Result<ArticleGetDto>>
    {
        private readonly ThoughtfulDbContext _context;
        private readonly IMapper _mapper;
        public UpdateArticleInfoHandler(ThoughtfulDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<Result<ArticleGetDto>> Handle(UpdateArticleInfos request, CancellationToken cancellationToken)
        {
            var articleById = await _context.Articles.FirstOrDefaultAsync(b => b.Id == request.ArticleId);

            if (articleById == null)
            {
                return await Task.FromResult(Result<ArticleGetDto>.Failure(new Error($"Article with id {request.ArticleId} not found", "NotFound")));
            }

            if (articleById != null)
                articleById.UpdateArticleInfo(request.ArticleDTO.Title, request.ArticleDTO.Subtitle, request.ArticleDTO.Body, request.ArticleDTO.NumberOfLikes, request.ArticleDTO.NumberOfShares);


            int result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return await Task.FromResult(Result<ArticleGetDto>.Success(_mapper.Map<ArticleGetDto>(articleById)));
            }
            return await Task.FromResult(Result<ArticleGetDto>.Failure(new Error("Error when updating article", "ErrorUpdating")));

        }
    }
}
