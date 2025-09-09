using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.DTO;

namespace Thoughtful.Api.Features.Authentication.Queries
{
    public class GetUserDataQuery : IRequest<Result<UserDataDTO>>
    {
    }
}
