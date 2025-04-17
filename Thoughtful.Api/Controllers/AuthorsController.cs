using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thoughtful.Api.Features.Author.Commands;
using Thoughtful.Api.Features.Author.Queries;
using Thoughtful.Api.Features.AuthorFeature;
using Thoughtful.Dal;

namespace Thoughtful.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ThoughtfulDbContext _context;
        protected IMediator _mediator;

        public AuthorsController(ThoughtfulDbContext context, IMediator mediator)
        {
            _context = context;
            this._mediator = mediator;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorGetDto>>> GetAuthors()
        {
            var authors = await this._mediator.Send(new GetAllAuthorsQuery());
            return Ok(authors);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorGetDto>> CreateAuthor(AuthorDto authorDto)
        {
            var result = await this._mediator.Send(new CreateAuthorCommand { Author = authorDto });
            if (result == null)
            {
                return BadRequest("Error");
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<AuthorGetDto>> GetAuthorById(int id)
        {
            var result = await _mediator.Send(new GetAuthorById { AuthorId = id });
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorDto updatedAuthor)
        {
            var updateAuthorResult = await _mediator.Send(new UpdateAuthor { AuthorId = id, AuthorDto = updatedAuthor });
            return Ok(updateAuthorResult);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _mediator.Send(new DeleteAuthor { AuthorId = id });
            return Ok(result);
        }

    }
}
