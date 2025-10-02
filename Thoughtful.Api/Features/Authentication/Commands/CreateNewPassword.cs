using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.DTO;

namespace Thoughtful.Api.Features.Authentication.Commands
{
    public class CreateNewPassword : IRequest<Result<string>>
    {
        public ResetPasswordDTO ResetPasswordDto { get; set; }
    }
}
