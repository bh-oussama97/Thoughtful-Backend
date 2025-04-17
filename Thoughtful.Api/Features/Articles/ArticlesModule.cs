using Codewrinkles.MinimalApi.SmartModules;
using Codewrinkles.MinimalApi.SmartModules.Extensions.SmartEndpointsExtensions;
using Thoughtful.Api.Features.Articles.Commands;
using Thoughtful.Api.Features.Articles.DTO;

namespace Thoughtful.Api.Features.Articles
{
    public class ArticlesModule : SmartModule
    {
        private readonly ILogger<ArticlesModule> _logger;
        public ArticlesModule(ILogger<ArticlesModule> logger)
        {
            _logger = logger;
        }
        public override IEndpointRouteBuilder MapEndpointDefinitions(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapSmartPost("/api/articles", async (ArticleDTO articleDto, IMediator mediator)
            => await CreateArticle(articleDto, mediator))
            .WithName("CreateArticle")
            .WithDisplayName("Articles")
            .WithTags("Articles")
            .Produces<ArticleGetDto>(201)
            .Produces(500);

            endpoints.MapSmartPut("/api/articles", async (ArticleDTO articleDto, IMediator mediator)
            => await UpdateArticle(articleDto, mediator))
            .WithName("UpdateArticle")
            .WithDisplayName("Articles")
            .WithTags("Articles")
            .Produces<ArticleGetDto>(201)
            .Produces(500);

            return endpoints;
        }

        /// <summary>
        /// Creates an article
        /// </summary>
        /// <param name="ArticleDto"></param>
        /// <returns></returns>
        private async Task<IResult> CreateArticle(ArticleDTO articleDt, IMediator mediator)
        {
            var command = new CreateArticle { Article = articleDt };
            var result = await mediator.Send(command);
            return Results.Created("CreateArticle", result);
        }

        private async Task<IResult> UpdateArticle(ArticleDTO authorToUpdate, IMediator mediator)
        {
            var command = new UpdateArticleInfos
            {
                ArticleDTO = authorToUpdate
            };
            var updateResult = await mediator.Send(command);

            return Results.Created("UpdateArticle", updateResult);
        }
    }
}
