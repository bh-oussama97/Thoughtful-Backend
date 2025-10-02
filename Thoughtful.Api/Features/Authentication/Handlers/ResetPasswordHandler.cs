using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.Commands;
using Thoughtful.Dal;
using Thoughtful.Dal.Email;
using Thoughtful.Domain.Model;
using RandomNumberGenerator = Thoughtful.Dal.RandomNumberGenerator;

namespace Thoughtful.Api.Features.Authentication.Handlers
{
    public class ResetPasswordHandler : IRequestHandler<Commands.ResetPassword, Result<string>>
    {
        private readonly ThoughtfulDbContext _ctx;

        private readonly UserManager<AppUser> _userManager;
        public ResetPasswordHandler(ThoughtfulDbContext datacontext, UserManager<AppUser> _userManager)
        {
            this._ctx = datacontext;
            this._userManager = _userManager;
        }
        public async Task<Result<string>> Handle(Commands.ResetPassword request, CancellationToken cancellationToken)
        {


            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            int codeReset = RandomNumberGenerator.Generate(100000, 999999);
            var resetPassword = new Domain.Model.ResetPassword()
            {
                Email = request.Email,
                ResetCode = codeReset.ToString(),
                Token = token,
                UserId = user.Id,
                InsertDateTimeUTC = DateTime.UtcNow
            };

            await _ctx.AddAsync(resetPassword);

            var result = await _ctx.SaveChangesAsync() > 0;

            string fromEmail = AppSettings.EmailParameters.fromEmail;
            string fromPassword = AppSettings.EmailParameters.fromPassword;
            EmailSender emailSender = new EmailSender(fromEmail, fromPassword);
            EmailModel emailModel = new EmailModel();
            emailModel.To = new List<string>();
            emailModel.Subject = "Reset Password Code";
            emailModel.To.Add(request.Email);
            emailModel.Body = "Hello "
            + request.Email + "<br><br>Please find the reset password token below<br><br><b>"
            + codeReset + "<b><br><br>Thanks<br>";

            await EmailSender.SendEmailAsync(emailModel);

            Result<string> response = new Result<string>();
            if (result)
            {

                response.IsSuccess = true;
                response.Body = "Token sent successfully in email";

            }

            else
            {

                response.IsSuccess = false;
                response.Body = "Failed to generate reset code";
            }

            return response;
        }
    }
}
