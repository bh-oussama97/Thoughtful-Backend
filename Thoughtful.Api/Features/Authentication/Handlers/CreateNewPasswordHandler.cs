using Microsoft.AspNetCore.Identity;
using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.Commands;
using Thoughtful.Dal;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Authentication.Handlers
{
    public class CreateNewPasswordHandler : IRequestHandler<CreateNewPassword, Result<string>>
    {
        private readonly ThoughtfulDbContext _ctx;

        private readonly UserManager<AppUser> _userManager;

        public CreateNewPasswordHandler(UserManager<AppUser> _userManager, ThoughtfulDbContext datacontext)
        {
            this._userManager = _userManager;
            this._ctx = datacontext;
        }
        public async Task<Result<string>> Handle(CreateNewPassword request, CancellationToken cancellationToken)
        {

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.ResetPasswordDto.Email);

            var resetPasswordDetails = await _ctx.ResetPasswords
                .Where(rp => rp.ResetCode == request.ResetPasswordDto.Otp && rp.UserId == user.Id)
                .OrderByDescending(rp => rp.InsertDateTimeUTC)
                .FirstOrDefaultAsync();
            Result<string> response = new Result<string>();


            if (resetPasswordDetails is not null)
            {
                // Verify if token is older than 15 minutes
                var expirationDateTimeUtc = resetPasswordDetails.InsertDateTimeUTC.AddMinutes(15);


                if (expirationDateTimeUtc < DateTime.UtcNow)
                {
                    response.IsSuccess = false;
                    response.Body = "Reset code is expired, please generate a new one !";
                }

                var res = await _userManager.ResetPasswordAsync(user, resetPasswordDetails.Token, request.ResetPasswordDto.NewPassword);


                if (res.Succeeded)
                {
                    response.IsSuccess = true;
                    response.Body = "Password Changed !";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Body = String.Join(",", res.Errors.Select(x => x.Description).ToList());
                }

            }
            else
            {
                response.IsSuccess = false;
                response.Body = "Reset code wrong , verify it !";
            }

            return response;
        }
    }
}
