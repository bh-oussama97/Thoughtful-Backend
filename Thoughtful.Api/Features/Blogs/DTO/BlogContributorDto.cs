namespace Thoughtful.Api.Features.Blogs.DTO
{
    public class BlogContributorDto
    {
        public int BlogId { get; set; }
        public string UserId { get; set; }

        public string Note { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }

        public DateTime ContributionDate { get; set; }

        // Optional: nested DTOs
        public UserDto User { get; set; }

    }

}
