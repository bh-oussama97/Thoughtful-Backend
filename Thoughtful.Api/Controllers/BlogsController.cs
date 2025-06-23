using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Author.Commands;
using Thoughtful.Api.Features.Author.Queries;
using Thoughtful.Api.Features.Blogs.Commands;
using Thoughtful.Api.Features.Blogs.DTO;
using Thoughtful.Api.Features.Blogs.Queries;
using Thoughtful.Dal;

namespace Thoughtful.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly ThoughtfulDbContext _context;
        protected IMediator _mediator;
        public BlogsController(ThoughtfulDbContext context, IMediator mediator)
        {
            _context = context;
            this._mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> AddContribution([FromForm] ContributionDTO contribution)
        {
            var result = await this._mediator.Send(new AddContributor { Contribution = contribution });
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<BlogGetDTO>>>> GetBlogs()
        {
            try
            {
                var result = await this._mediator.Send(new GetAllBlogs());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Result<List<BlogGetDTO>>.Failure(new Error($"Exception: {ex.Message}", "Exception")));

            }

        }
        [HttpGet]
        public async Task<ActionResult> GetBlogById(int id)
        {
            var result = await this._mediator.Send(new GetBlogById { BlogId = id });

            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateBlog(BlogDTO blogToAdd)
        {
            var result = await this._mediator.Send(new CreateBlog { NewBlog = blogToAdd });
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveBlogContributor(int BlogId, int ContributorId)
        {
            var result = await this._mediator.Send(new RemoveBlogContributor { BlogId = BlogId, ContributorId = ContributorId });
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBlogInfos(int BlogId, BlogDTO blog)
        {
            var result = await this._mediator.Send(new UpdateBlogInfo { BlogId = BlogId, Blog = blog });
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBlogOwner(int BlogId, int OwnerId)
        {
            var result = await this._mediator.Send(new UpdateBlogOwner { BlogId = BlogId, OnwerId = OwnerId });
            return Ok(result);
        }

        //GET /api/authors/{id}/ blogs → Get all blogs owned by an author

        [HttpGet]
        public async Task<ActionResult> GetBlogsByAuthor(int authorId)
        {
            var result = await this._mediator.Send(new GetBlogsByAuthorQuery { AuthorId = authorId });
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetFile([FromQuery] string filename)
        {

            var command = new DownloadBlogFileCommand { Filename = filename };
            return await _mediator.Send(command);

        }

        [HttpGet]
        public async Task<ActionResult> ExportXLS()
        {

            var result = await this._mediator.Send(new ExportXLSQuery { });
            if (result != null && result.IsSuccess)
                return File(result.Body, "application/xlsx", $"Template-{DateTime.Now.ToString("yyyyMMddTHHmm")}.xlsx");
            else
                return Ok(result);
        }
    }
}
