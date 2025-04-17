using Thoughtful.Api.Features.Articles.DTO;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Articles.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleDTO, Article>().ReverseMap();
            CreateMap<Article, ArticleGetDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        }
    }
}
