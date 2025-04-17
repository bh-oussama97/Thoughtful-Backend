using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.Commands;
using Thoughtful.Api.Features.Blogs.DTO;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Blogs.Handlers;

public class UpdateBlogOwnerHandler : IRequestHandler<UpdateBlogOwner, Result<BlogGetDTO>>
{
    private readonly ThoughtfulDbContext _ctx;
    private readonly IMapper _mapper;
    public UpdateBlogOwnerHandler(ThoughtfulDbContext ctx, IMapper mapper)
    {
        _ctx = ctx;
        _mapper = mapper;
    }
    public async Task<Result<BlogGetDTO>> Handle(UpdateBlogOwner request, CancellationToken cancellationToken)
    {
        var blog = await _ctx.Blogs.Include(b => b.Contributors)
            .FirstOrDefaultAsync(b => b.Id == request.BlogId);

        var newOwner = await _ctx.Authors.FirstOrDefaultAsync(a => a.Id == request.OnwerId);

        // Ensure new owner is in Contributors list
        if (blog.Contributors.Any(c => c.Id == request.OnwerId))
        {
            blog.Contributors.Add(newOwner);
            await _ctx.SaveChangesAsync();
            return Result<BlogGetDTO>.Success(_mapper.Map<BlogGetDTO>(blog));
        }
        return Result<BlogGetDTO>.Failure(new Error("Error when updating blog owner"));
    }
}
