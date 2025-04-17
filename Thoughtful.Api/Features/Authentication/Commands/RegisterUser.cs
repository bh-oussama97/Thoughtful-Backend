using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.DTO;

namespace Thoughtful.Api.Features.Authentication.Commands
{
    public class RegisterUser : IRequest<Result<UserGetDTO>>
    {
        public RegisterDTO RegisterRequest { get; set; }
    }
}
