namespace Thoughtful.Api.Features.Articles.DTO
{
    public class ArticleDTO
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Body { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfShares { get; set; }
    }
}
