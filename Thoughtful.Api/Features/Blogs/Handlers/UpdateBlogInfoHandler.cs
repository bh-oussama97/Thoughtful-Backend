using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.Commands;
using Thoughtful.Api.Features.Blogs.DTO;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Blogs.Handlers
{
    public class UpdateBlogInfoHandler : IRequestHandler<UpdateBlogInfo, Result<BlogGetDTO>>
    {
        private readonly ThoughtfulDbContext _ctx;
        protected IMapper _mapper;
        public UpdateBlogInfoHandler(ThoughtfulDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<Result<BlogGetDTO>> Handle(UpdateBlogInfo request, CancellationToken cancellationToken)
        {
            var blog = await _ctx.Blogs.FirstOrDefaultAsync(b => b.Id == request.BlogId);

            if (blog != null)
                blog.UpdateBlogInfo(request.Blog.Name, request.Blog.Description);

            int saveChangesResult = await _ctx.SaveChangesAsync();
            if (saveChangesResult == 0)
            {
                return await Task.FromResult(Result<BlogGetDTO>.Failure(new Error($"Error when updating blog with id {request.BlogId}")));
            }
            return await Task.FromResult(Result<BlogGetDTO>.Success(_mapper.Map<BlogGetDTO>(blog)));

        }
    }
}
