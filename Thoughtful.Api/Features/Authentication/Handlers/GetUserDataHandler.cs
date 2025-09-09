using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Authentication.DTO;
using Thoughtful.Api.Features.Authentication.Queries;
using Thoughtful.Dal;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Authentication.Handlers
{
    public class GetUserDataHandler : IRequestHandler<GetUserDataQuery, Result<UserDataDTO>>
    {
        private readonly ThoughtfulDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetUserDataHandler(
            ThoughtfulDbContext context,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Result<UserDataDTO>> Handle(GetUserDataQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Result<UserDataDTO>.Failure(new Error("Unauthorized",""));

            var user = await _context.Users
                .Include(u => u.UserPhotos)
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user == null)
                return Result<UserDataDTO>.Failure(new Error("User not found", ""));

            // 3. Map to DTO
            var dto = _mapper.Map<UserDataDTO>(user);

            // 4. Load avatar file if exists
            var latestPhoto = user.UserPhotos.OrderByDescending(p => p.Id).FirstOrDefault();
            if (latestPhoto != null && !string.IsNullOrEmpty(latestPhoto.FileName))
            {
                string profilePhotosRoot = AppSettings.ProfilePhotosPath;
                string filePath = Path.Combine(profilePhotosRoot, latestPhoto.FileName);

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

            return Result<UserDataDTO>.Success(dto);
        }
    }
}
