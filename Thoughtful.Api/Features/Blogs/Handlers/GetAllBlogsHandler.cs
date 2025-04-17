using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.DTO;
using Thoughtful.Api.Features.Blogs.Queries;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Blogs.Handlers
{
    public class GetAllBlogsHandler : IRequestHandler<GetAllBlogs, Result<List<BlogGetDTO>>>
    {
        private readonly ThoughtfulDbContext _ctx;
        private readonly IMapper _mapper;
        public GetAllBlogsHandler(ThoughtfulDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<Result<List<BlogGetDTO>>> Handle(GetAllBlogs request, CancellationToken cancellationToken)
        {
            try
            {
                //var blogs = await _ctx.Blogs.ToListAsync();
                //var blogDtos = _mapper.Map<List<BlogGetDTO>>(blogs);
                //return await Task.FromResult(Result<List<BlogGetDTO>>.Success(blogDtos));

                var makes = _ctx.Blogs.AsQueryable();

                var vehicleMakes = await makes.Include(m => m.Contributors).ToListAsync();

                return await Task.FromResult(Result<List<BlogGetDTO>>.Success(_mapper.Map<List<BlogGetDTO>>(vehicleMakes)));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(Result<List<BlogGetDTO>>.Failure(new Error($"Exception: {ex.Message}", "Exception")));

            }

        }
    }
}
