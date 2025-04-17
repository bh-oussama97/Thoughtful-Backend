using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.DTO;
using Thoughtful.Api.Features.Blogs.Queries;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Blogs.Handlers
{
    public class GetBlogByIdHandler : IRequestHandler<GetBlogById, Result<BlogGetDTO>>
    {
        private readonly ThoughtfulDbContext _ctx;
        private readonly IMapper _mapper;

        public GetBlogByIdHandler(ThoughtfulDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public async Task<Result<BlogGetDTO>> Handle(GetBlogById request, CancellationToken cancellationToken)
        {
            var blog = await _ctx.Blogs.Where(b => b.Id == request.BlogId).Include(x => x.Contributors).FirstOrDefaultAsync();

            if (blog is null)
            {
                return await Task.FromResult(Result<BlogGetDTO>.Failure(new Error("Blog not found")));
            }
            return await Task.FromResult(Result<BlogGetDTO>.Success(_mapper.Map<BlogGetDTO>(blog)));
        }
    }
}
