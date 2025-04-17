

using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Categories.Commands;
using Thoughtful.Api.Features.Categories.Dto;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Categories.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<AddCategory, Result<CategoryGetDto>>
    {
        private readonly ThoughtfulDbContext _ctx;
        private readonly IMapper _mapper;
        public CreateCategoryHandler(ThoughtfulDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<Result<CategoryGetDto>> Handle(AddCategory request, CancellationToken cancellationToken)
        {
            var categoryToAdd = _mapper.Map<Domain.Model.Category>(request.Category);
            var categoryByName = _ctx.Categories.Where(x => x.Name == categoryToAdd.Name).FirstOrDefault();
            if (categoryByName is not null)
            {
                return await Task.FromResult(Result<CategoryGetDto>.Failure(new Error("Category Exists", "InvalidNameError")));
            }
            _ctx.Categories.Add(categoryToAdd);
            await _ctx.SaveChangesAsync();
            CategoryGetDto result = _mapper.Map<CategoryGetDto>(categoryToAdd);
            return await Task.FromResult(Result<CategoryGetDto>.Success(result));
        }
    }
}
