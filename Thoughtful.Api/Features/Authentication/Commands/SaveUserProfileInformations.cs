using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.DTO;

namespace Thoughtful.Api.Features.Authentication.Commands
{
    public class SaveUserProfileInformations :  IRequest<Result<UserGetDTO>>
    {
        public  UserProfileDTO  UserProfile { get; set; }
    }
}
