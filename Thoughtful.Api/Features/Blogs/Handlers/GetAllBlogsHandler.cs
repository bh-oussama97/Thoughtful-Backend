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
                string filepath = AppSettings.UploadFilePath;
                var blogDtos = await _ctx.Blogs
                    .Include(x => x.Contributors)
                    .ThenInclude(c => c.User)
                    .ToListAsync(cancellationToken);

                var mappedBlogs = _mapper.Map<List<BlogGetDTO>>(blogDtos);

                return Result<List<BlogGetDTO>>.Success(mappedBlogs);
            }
            catch (Exception ex)
            {
                var fullMessage = $"{ex.Message}\n{ex.InnerException?.Message}";
                return Result<List<BlogGetDTO>>.Failure(new Error($"Exception: {fullMessage}", "Exception"));
            }

        }

    }
}
