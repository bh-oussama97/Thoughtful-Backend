using Microsoft.AspNetCore.Identity;
using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.Commands;
using Thoughtful.Api.Features.Authentication.DTO;
using Thoughtful.Dal;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Authentication.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUser, Result<UserGetDTO>>
    {
        private readonly ThoughtfulDbContext _ctx;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _siginInmanager;
        private readonly JwtService _JwtService;

        public LoginUserHandler(UserManager<AppUser> userManager, JwtService jwtService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _JwtService = jwtService;
            _siginInmanager = signInManager;
        }
        public async Task<Result<UserGetDTO>> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            Result<UserGetDTO> commandresult = new Result<UserGetDTO>();

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.LoginRequest.Email);

            if (user == null)

            {
                return Result<UserGetDTO>.Failure(new Error("Unauthorized", "UserNotFound"));
            }

            var result = await _siginInmanager.CheckPasswordSignInAsync(user, request.LoginRequest.Password, false);

            if (result.Succeeded)
            {
                commandresult.IsSuccess = true;
                commandresult.Body = new UserGetDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = _JwtService.createToken(user)
                };
            }
            else
            {
                commandresult.IsSuccess = false;
                commandresult.Error = new Error("Password is wrong");
            }
            return commandresult;
        }
    }
}
