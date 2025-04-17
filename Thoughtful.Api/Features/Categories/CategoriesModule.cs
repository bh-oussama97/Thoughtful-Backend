using Codewrinkles.MinimalApi.SmartModules;
using Codewrinkles.MinimalApi.SmartModules.Extensions.SmartEndpointsExtensions;
using Thoughtful.Api.Features.AuthorFeature;
using Thoughtful.Api.Features.Categories.Commands;
using Thoughtful.Api.Features.Categories.Dto;
using Thoughtful.Api.Features.Categories.Queries;

namespace Thoughtful.Api.Features.Categories
{
    public class CategoriesModule : SmartModule

    {
        private readonly ILogger<AuthorModule> _logger;
        public CategoriesModule(ILogger<AuthorModule> logger)
        {
            _logger = logger;
        }


        public override IEndpointRouteBuilder MapEndpointDefinitions(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapSmartGet("/api/categories", async (IMediator mediator)
    => await GetAllCategories(mediator))
    .WithName("GetAllCategories")
    .WithDisplayName("Categories")
    .WithTags("Categories")
    .Produces<List<CategoryGetDto>>()
    .Produces(500);
            endpoints.MapSmartPost("/api/categories", async (CategoryDto authorDto, IMediator mediator)
             => await AddCategory(authorDto, mediator))
             .WithName("AddCategory")
             .WithDisplayName("Categories")
             .WithTags("Categories")
             .Produces<CategoryGetDto>(200)
             .Produces(500);

            endpoints.MapSmartGet("api/categories/{id}", async (int id, IMediator mediator)
                => await GetCategoryById(id, mediator))
                .WithName("GetCategoryById")
                .WithDisplayName("Categories")
                .Produces<CategoryGetDto>()
                .Produces(404)
                .Produces(500);
            return endpoints;
        }

        /// <summary>
        /// Gets all authors
        /// </summary>
        /// <returns></returns>
        private async Task<IResult> GetAllCategories(IMediator mediator)
        {
            var categories = await mediator.Send(new GetAllCategoriesQuery());
            return Results.Ok(categories);
        }
        /// <summary>
        /// Add a category
        /// </summary>
        /// <param name="categoryDto"></param>
        /// <returns></returns>
        private async Task<IResult> AddCategory(CategoryDto categoryDto, IMediator mediator)
        {
            var command = new AddCategory { Category = categoryDto };
            var result = await mediator.Send(command);
            return Results.Ok(result);
        }

        private async Task<IResult> GetCategoryById(int id, IMediator mediator)
        {
            var result = await mediator.Send(new GetCategoryById { CategoryId = id });
            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
        }
    }
}
