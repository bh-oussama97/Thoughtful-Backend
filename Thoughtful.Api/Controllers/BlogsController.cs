using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> AddContributor(int BlogId, int ContributorId)
        {
            var result = await this._mediator.Send(new AddContributor { BlogId = BlogId, ContributorId = ContributorId });
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetBlogs()
        {
            var result = await this._mediator.Send(new GetAllBlogs());
            return Ok(result);
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
    }
}
