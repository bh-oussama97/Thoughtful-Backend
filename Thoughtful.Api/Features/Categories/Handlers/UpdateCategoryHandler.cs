using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Categories.Commands;
using Thoughtful.Api.Features.Categories.Dto;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Categories.Handlers
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategory, Result<CategoryGetDto>>
    {
        private readonly ThoughtfulDbContext _ctx;
        private readonly IMapper _mapper;
        public UpdateCategoryHandler(ThoughtfulDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<Result<CategoryGetDto>> Handle(UpdateCategory request, CancellationToken cancellationToken)
        {
            var categoryById = await _ctx.Categories.FirstOrDefaultAsync(b => b.Id == request.Id);

            if (categoryById == null)
            {
                return await Task.FromResult(Result<CategoryGetDto>.Failure(new Error($"Category with id {request.Id} not found", "NotFound")));
            }

            if (categoryById != null)
            {
                categoryById.Name = request.Category.Name;
                categoryById.Description = request.Category.Description;

            }
            int result = await _ctx.SaveChangesAsync();

            if (result > 0)
            {
                return await Task.FromResult(Result<CategoryGetDto>.Success(_mapper.Map<CategoryGetDto>(categoryById)));
            }
            return await Task.FromResult(Result<CategoryGetDto>.Failure(new Error("Error when updating category", "ErrorUpdating")));
        }
    }
}
