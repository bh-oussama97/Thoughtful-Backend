using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Identity;
using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.Commands;
using Thoughtful.Api.Features.Authentication.DTO;
using Thoughtful.Dal;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Authentication.Handlers
{
    public class LoginUserHandler : IRequestHandler<LoginUser, Result<UserDataDTO>>
    {
        private readonly ThoughtfulDbContext _ctx;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _siginInmanager;
        private readonly JwtService _JwtService;

        public LoginUserHandler(UserManager<AppUser> userManager, JwtService jwtService, SignInManager<AppUser> signInManager,ThoughtfulDbContext ctx)
        {
            _userManager = userManager;
            _JwtService = jwtService;
            _siginInmanager = signInManager;
            _ctx = ctx;
        }
        public async Task<Result<UserDataDTO>> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            Result<UserDataDTO> commandresult = new Result<UserDataDTO>();

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == request.LoginRequest.Email);


            if (user == null)

            {
                return Result<UserDataDTO>.Failure(new Error("Unauthorized", "UserNotFound"));
            }

            var result = await _siginInmanager.CheckPasswordSignInAsync(user, request.LoginRequest.Password, false);

            UserDataDTO dto = new UserDataDTO
            {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                Token = _JwtService.createToken(user),
            };

            string _uploadRoot = AppSettings.ProfilePhotosPath;

            var userAvtarPhoto = _ctx.UserProfilePhotos.FirstOrDefault(el=>el.UserId == user.Id);

            if (userAvtarPhoto != null && !string.IsNullOrEmpty(userAvtarPhoto.FileName))
            {
                string filePath = Path.Combine(_uploadRoot, userAvtarPhoto.FileName);

                if (File.Exists(filePath))
                {
                    var memory = new MemoryStream();
                    await using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        await stream.CopyToAsync(memory, cancellationToken);
                    }
                    memory.Position = 0;

                    // Convert to Base64 so it can be safely returned in JSON
                    var bytes = memory.ToArray();
                    var base64 = Convert.ToBase64String(bytes);
                    var contentType = FileManager.GetContentType(filePath);

                    dto.Avatar = $"data:{contentType};base64,{base64}";
                }
            }


            if (result.Succeeded)
            {
                commandresult.IsSuccess = true;
                commandresult.Body = dto;
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
