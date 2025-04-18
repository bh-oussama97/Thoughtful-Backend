using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.Commands;
using Thoughtful.Api.Features.Articles.DTO;
using Thoughtful.Dal;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Articles.Handlers
{
    public class CreateArticleHandler : IRequestHandler<CreateArticle, Result<ArticleGetDto>>
    {
        private readonly ThoughtfulDbContext _context;
        private readonly IMapper _mapper;
        public CreateArticleHandler(ThoughtfulDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<Result<ArticleGetDto>> Handle(CreateArticle request, CancellationToken cancellationToken)
        {
            var articleByTitle = await _context.Articles.Where(a => a.Title.Equals(request.Article.Title)).FirstOrDefaultAsync();
            if (articleByTitle is not null)
            {
                return await Task.FromResult(Result<ArticleGetDto>.Failure(new Error($"Article with title {request.Article.Title} Already exists", "ArticleAlreadyExists")));
            }
            var newArticle = Article.CreateArticle(request.Article.Title, request.Article.Subtitle,
                request.Article.Body, request.Article.AuthorId, request.Article.NumberOfLikes, request.Article.NumberOfShares);
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == request.Article.AuthorId);
            var category = await _context.Categories.FirstOrDefaultAsync(a => a.Id == request.Article.CategoryId);
            newArticle.Author = author;
            newArticle.Category = category;
            _context.Articles.Add(newArticle);
            int result = await _context.SaveChangesAsync();

            return await Task.FromResult(Result<ArticleGetDto>.Success(_mapper.Map<ArticleGetDto>(newArticle)));
        }
    }
}
