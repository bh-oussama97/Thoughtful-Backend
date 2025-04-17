using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Categories.Dto;
using Thoughtful.Api.Features.Categories.Queries;
using Thoughtful.Dal;

namespace Thoughtful.Api.Features.Categories.Handlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<CategoryGetDto>>>
    {
        private readonly ThoughtfulDbContext _ctx;
        private readonly IMapper _mapper;
        public GetAllCategoriesHandler(ThoughtfulDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        public async Task<Result<List<CategoryGetDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoriesList = await _ctx.Categories.ToListAsync();
            if (categoriesList.Count == 0)
            {
                return await Task.FromResult(Result<List<CategoryGetDto>>.Failure(new Error("Categories List is empty", "ListEmpty")));
            }

            Result<List<CategoryGetDto>> result = new Result<List<CategoryGetDto>>();
            result.IsSuccess = true;
            result.Body = _mapper.Map<List<CategoryGetDto>>(categoriesList);
            return await Task.FromResult(result);
        }
    }
}
