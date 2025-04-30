namespace Thoughtful.Api.Features.Blogs.DTO
{
    public class ContributionFileDto
    {
        public int BlogId { get; set; }
        public string ContributorId { get; set; }
        public string Note { get; set; }

        public string File { get; set; }

        public ContributionFileDto()
        {
        }
    }
}
