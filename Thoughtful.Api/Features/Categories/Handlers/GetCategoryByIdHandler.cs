using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Categories.Dto;
using Thoughtful.Api.Features.Categories.Queries;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Categories.Handlers
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryById, Result<CategoryGetDto>>
    {
        private readonly ThoughtfulDbContext _ctx;
        private readonly IMapper _mapper;
        public GetCategoryByIdHandler(ThoughtfulDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<Result<CategoryGetDto>> Handle(GetCategoryById request, CancellationToken cancellationToken)
        {
            var category = await _ctx.Categories.FirstOrDefaultAsync(a => a.Id == request.CategoryId);
            if (category is null)
            {
                return await Task.FromResult(Result<CategoryGetDto>.Failure(new Error("Category not found", "NotFound")));
            }
            return await Task.FromResult(Result<CategoryGetDto>.Success(_mapper.Map<CategoryGetDto>(category)));
        }
    }
}
