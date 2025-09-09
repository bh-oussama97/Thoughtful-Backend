using Microsoft.AspNetCore.Identity;
using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.Commands;
using Thoughtful.Api.Features.Authentication.DTO;
using Thoughtful.Dal;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Authentication.Handlers
{
    public class SaveUserProfileInformationsHandler : IRequestHandler<SaveUserProfileInformations, Result<UserGetDTO>>
    {
        private readonly ThoughtfulDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;


        public SaveUserProfileInformationsHandler(ThoughtfulDbContext context, IMapper mapper, UserManager<AppUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<Result<UserGetDTO>> Handle(SaveUserProfileInformations request, CancellationToken cancellationToken)
        {
            string _uploadRoot = AppSettings.ProfilePhotosPath;
            string fileName = "";

            var user = await _context.Users
                .Include(u => u.UserPhotos)
                .FirstOrDefaultAsync(u => u.Id == request.UserProfile.Id, cancellationToken);

            if (user == null)
                return Result<UserGetDTO>.Failure(new Error("NotFound", "User not found"));

            user.UserName = request.UserProfile.UserName ?? user.UserName;
            user.Email = request.UserProfile.Email ?? user.Email;


            if (request.UserProfile.Avatar.Length > 10 * 1024 * 1024)
            {
                return Result<UserGetDTO>.Failure(new Error("File size exceeds 10MB limit", "FileTooLarge"));
            }
            if (request.UserProfile.Avatar != null && request.UserProfile.Avatar.Length > 0)
            {
                Directory.CreateDirectory(_uploadRoot);

                fileName = Path.GetFileNameWithoutExtension(request.UserProfile.Avatar.FileName)
                    .Replace(" ", "_")
                    .Replace("(", "")
                    .Replace(")", "")
                    + Path.GetExtension(request.UserProfile.Avatar.FileName);

                var filePath = Path.Combine(_uploadRoot, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.UserProfile.Avatar.CopyToAsync(stream);
                }

                if (!File.Exists(filePath))
                {
                    return Result<UserGetDTO>.Failure(new Error("Failed to save file to disk", "FileSaveError"));
                }
            }


            user.UserPhotos.Add(new UserProfilePhoto
            {
                Id = Guid.NewGuid().ToString(),
                FileName = fileName,
                UserId = user.Id
            });

            if (!string.IsNullOrEmpty(request.UserProfile.OldPassword)
                && !string.IsNullOrEmpty(request.UserProfile.NewPassword))
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, request.UserProfile.OldPassword);
                if (!passwordCheck)
                {
                    return Result<UserGetDTO>.Failure(new Error("Old password is incorrect", ""));
                }

                var passwordResult = await _userManager.ChangePasswordAsync(user, request.UserProfile.OldPassword, request.UserProfile.NewPassword);
                if (!passwordResult.Succeeded)
                {
                    var errors = string.Join("; ", passwordResult.Errors.Select(e => e.Description));
                    return Result<UserGetDTO>.Failure(new Error($"Password change failed: {errors}", $""));
                }
            }

            int success = await _context.SaveChangesAsync(cancellationToken);
            if (success > 0)
                return Result<UserGetDTO>.Failure(new Error("Problem saving user profile", "NotFound"));

            var dto = _mapper.Map<UserGetDTO>(user);
            return Result<UserGetDTO>.Success(dto);
        }
    }
}
