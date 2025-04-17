using Microsoft.AspNetCore.Identity;
using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.Commands;
using Thoughtful.Api.Features.Authentication.DTO;
using Thoughtful.Dal;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Authentication.Handlers
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, Result<UserGetDTO>>
    {
        private readonly IMapper mapper;
        private readonly UserManager<AppUser> userManager;
        private readonly JwtService jwtservice;
        public RegisterUserHandler(IMapper mapper,
        UserManager<AppUser> userManager, JwtService tokenService
        )
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.jwtservice = tokenService;
        }
        public async Task<Result<UserGetDTO>> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            Result<UserGetDTO> commandResult = new Result<UserGetDTO>();

            var passengerToAdd = mapper.Map<AppUser>(request.RegisterRequest);

            passengerToAdd.UserName = request.RegisterRequest.Username;
            passengerToAdd.Email = request.RegisterRequest.Email;
            passengerToAdd.Id = Guid.NewGuid().ToString();


            if (userManager.Users.Any(x => x.Email == request.RegisterRequest.Email))
            {
                return Result<UserGetDTO>.Failure(new Error("Email already taken", "EmailTaken"));
            }


            var result = await userManager.CreateAsync(passengerToAdd, request.RegisterRequest.Password);


            if (!result.Succeeded)

            {
                foreach (var err in result.Errors)
                {
                    commandResult.Error = new Error(err.Description, err.Description);
                    commandResult.IsSuccess = false;
                }

                return commandResult;
            }
            else
            {
                commandResult.IsSuccess = true;
                commandResult.Body = new UserGetDTO
                {
                    Email = passengerToAdd.Email,
                    UserName = passengerToAdd.UserName,
                    Token = jwtservice.createToken(passengerToAdd)
                };
                return commandResult;
            }
        }
    }
}
