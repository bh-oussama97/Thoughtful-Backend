using Thoughtful.Api.Features.Authentication.DTO;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Authentication.Profiles
{
    public class AuthenticationProfile : Profile
    {
        public AuthenticationProfile()
        {
            CreateMap<UserGetDTO, AppUser>().ReverseMap();
            CreateMap<RegisterDTO, AppUser>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Username))
            .ForMember(u => u.Email, opt => opt.MapFrom(x => x.Email));
        }
    }
}
