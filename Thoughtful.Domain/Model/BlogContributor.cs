namespace Thoughtful.Domain.Model
{
    public class BlogContributor
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }

        public string Note { get; set; }
        public string Filename { get; set; }
        public string Extension { get; set; }
        public DateTime ContributionDate { get; set; }
    }
}
