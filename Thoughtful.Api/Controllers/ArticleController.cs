using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Articles.Commands;
using Thoughtful.Api.Features.Articles.DTO;
using Thoughtful.Api.Features.Articles.Queries;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        protected IMediator _mediator;

        public ArticleController(IMediator mediator)
        {
            this._mediator = mediator;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles()
        {
            var articles = await this._mediator.Send(new GetAllArticlesQuery());
            return Ok(articles);
        }

        [HttpGet]
        public async Task<ActionResult<Article>> GetArticle(int id)
        {
            var article = await this._mediator.Send(new GetArticleByIdQuery { Id = id });
            return Ok(article);
        }

        [HttpPost]
        public async Task<ActionResult<Result<ArticleGetDto>>> CreateArticle(ArticleDTO article)
        {
            try
            {
                var command = new CreateArticle { Article = article };
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Result<ArticleGetDto>.Failure(new Error($"Exception: {ex.Message}", "Exception")));

            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArticle(int id, ArticleDTO updatedArticle)
        {
            var updateArticleCommand = await _mediator.Send(new UpdateArticleInfos { ArticleId = id, ArticleDTO = updatedArticle });
            return Ok(updateArticleCommand);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var result = await _mediator.Send(new DeleteArticle { ArticleId = id });
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> SetAuthor(int id, int authorId)
        {
            var result = await _mediator.Send(new SetAuthor { AuthorId = authorId, Id = id });
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> AddCategory(int id, int categoryId)
        {
            var result = await _mediator.Send(new AddCategoryToArticle { CategoryId = categoryId, Id = id });
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> RemoveCategory(int id, int categoryId)
        {
            var result = await _mediator.Send(new RemoveCategoryFromArticle { ArticleId = id, CategoryId = categoryId });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByAuthor(int authorId)
        {
            var result = await this._mediator.Send(new GetAllArticlesByAuthorQuery { AuthorId = authorId });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var result = await this._mediator.Send(new GetAllByCategory { CategoryId = categoryId });
            return Ok(result);
        }
    }
}
