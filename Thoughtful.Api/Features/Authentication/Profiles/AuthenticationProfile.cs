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

            CreateMap<AppUser, UserDataDTO>()
                // Map Avatar from latest photo if available, otherwise null/default
                .ForMember(dest => dest.Avatar,
                    opt => opt.MapFrom(src =>
                        src.UserPhotos
                            .OrderByDescending(p => p.Id)   // assumes Id is sequential/Guid
                            .Select(p => p.FileName)
                            .FirstOrDefault()
                    ))
                // Token still needs to be set manually
                .ForMember(dest => dest.Token, opt => opt.Ignore());
        }
    }
}
