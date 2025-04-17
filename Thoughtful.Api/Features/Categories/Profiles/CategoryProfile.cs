using Thoughtful.Api.Features.Categories.Dto;

namespace Thoughtful.Api.Features.Categories.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, Thoughtful.Domain.Model.Category>().ReverseMap();
            CreateMap<Thoughtful.Domain.Model.Category, CategoryGetDto>();
        }
    }
}
