namespace Thoughtful.Api.Features.Blogs.DTO
{
    public class ContributionDTO
    {
        public int BlogId { get; set; }
        public string ContributorId { get; set; }
        public string Note { get; set; }

        public IFormFile File { get; set; }

        public ContributionDTO()
        {
        }

    }
}
