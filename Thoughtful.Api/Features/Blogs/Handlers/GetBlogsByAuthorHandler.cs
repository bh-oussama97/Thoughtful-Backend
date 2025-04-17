using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.DTO;
using Thoughtful.Api.Features.Blogs.Queries;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Blogs.Handlers
{
    public class GetBlogsByAuthorHandler : IRequestHandler<GetBlogsByAuthorQuery, Result<List<BlogGetDTO>>>
    {
        private readonly ThoughtfulDbContext _ctx;
        private readonly IMapper _mapper;
        public GetBlogsByAuthorHandler(ThoughtfulDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<Result<List<BlogGetDTO>>> Handle(GetBlogsByAuthorQuery request, CancellationToken cancellationToken)
        {
            var blogsByAuthor = await _ctx.Blogs
          .Where(a => a.Contributors.Any(c => c.Id == request.AuthorId))
          .Select(b => new BlogGetDTO(b)).ToListAsync(cancellationToken);
            return await Task.FromResult(Result<List<BlogGetDTO>>.Success(blogsByAuthor));
        }
    }
}
