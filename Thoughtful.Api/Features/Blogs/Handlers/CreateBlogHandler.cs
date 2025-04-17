using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.Commands;
using Thoughtful.Api.Features.Blogs.DTO;
using Thoughtful.Dal;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Blogs.Handlers
{
    public class CreateBlogHandler : IRequestHandler<CreateBlog, Result<BlogGetDTO>>
    {
        private readonly ThoughtfulDbContext _ctx;
        private readonly IMapper _mapper;

        public CreateBlogHandler(ThoughtfulDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<Result<BlogGetDTO>> Handle(CreateBlog request, CancellationToken cancellationToken)
        {
            var blogByName = _ctx.Blogs.Where(x => x.Name.ToLower().Equals(request.NewBlog.Name.ToLower())).FirstOrDefaultAsync();
            if (blogByName is not null)
            {
                return await Task.FromResult(Result<BlogGetDTO>.Failure(new Error($"Blog with the name : {request.NewBlog.Name} already exists !")));
            }
            var newBlog = new BlogDTO(request.NewBlog.Name, request.NewBlog.Description);
            var blogMapped = _mapper.Map<Blog>(newBlog);
            blogMapped.CreatedDate = DateTime.Now;
            //blogMapped.SetOwner(author);
            _ctx.Blogs.Add(blogMapped);
            int result = await _ctx.SaveChangesAsync();
            if (result > 0)
            {
                return Result<BlogGetDTO>.Success(_mapper.Map<BlogGetDTO>(blogMapped));

            }
            return Result<BlogGetDTO>.Failure(new Error("Error when creating blog"));
        }
    }
}
