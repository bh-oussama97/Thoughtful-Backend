namespace Thoughtful.Api.Features.Authentication.DTO
{
    public class UserProfileDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public IFormFile Avatar { get; set; }

    }
}   
