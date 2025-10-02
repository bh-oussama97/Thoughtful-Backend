using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.DTO;

namespace Thoughtful.Api.Features.Authentication.Commands
{
    public class ResetPassword : IRequest<Result<string>>
    {
        public string Email { get; set; }

    }
}
