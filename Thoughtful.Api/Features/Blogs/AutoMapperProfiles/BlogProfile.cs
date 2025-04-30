using Thoughtful.Api.Features.Blogs.DTO;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Blogs.AutoMapperProfiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogGetDTO>()
              .ForMember(dest => dest.Contributors, opt => opt.MapFrom(src => src.Contributors));

            CreateMap<BlogContributor, BlogContributorDto>()
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<AppUser, UserDto>().ReverseMap();

        }

    }
}
