using Thoughtful.Api.Features.Blogs.DTO;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Blogs.AutoMapperProfiles
{
    public class BlogWriteProfiles : Profile
    {
        public BlogWriteProfiles()
        {
            CreateMap<Blog, BlogDTO>().ReverseMap();
            CreateMap<Blog, BlogGetDTO>().ReverseMap();
        }
    }
}
