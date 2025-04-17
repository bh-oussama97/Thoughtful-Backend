using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.DTO;

namespace Thoughtful.Api.Features.Authentication.Commands
{
    public class LoginUser : IRequest<Result<UserGetDTO>>
    {
        public LoginRequestDTO LoginRequest { get; set; }
    }
}
