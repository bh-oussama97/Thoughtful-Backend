using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thoughtful.Api.Features.Categories.Commands;
using Thoughtful.Api.Features.Categories.Dto;
using Thoughtful.Api.Features.Categories.Queries;
using Thoughtful.Dal;

namespace Thoughtful.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        protected IMediator _mediator;
        protected ThoughtfulDbContext _context;
        public CategoryController(IMediator mediator, ThoughtfulDbContext context)
        {
            this._mediator = mediator;
            this._context = context;
        }

        // GET: api/category
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await this._mediator.Send(new GetAllCategoriesQuery());
            return Ok(categories);
        }

        // GET: api/category/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var result = await this._mediator.Send(new GetCategoryById { CategoryId = id });
            if (result is null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/category
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto category)
        {
            var command = new AddCategory { Category = category };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        // PUT: api/category/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto updatedCategory)
        {
            var updateCommand = new UpdateCategory { Id = id, Category = updatedCategory };
            var result = await _mediator.Send(updateCommand);
            return Ok(result);
        }

        // DELETE: api/category/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var removeCategory = new RemoveCategory { CategoryId = id };
            var result = await _mediator.Send(removeCategory);

            return Ok(result);

        }
    }
}
