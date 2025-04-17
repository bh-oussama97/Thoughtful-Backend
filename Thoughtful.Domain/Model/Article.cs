namespace Thoughtful.Domain.Model
{
    public class Article
    {
        public Article()
        {
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Body { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfShares { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Article(string title, string? subtitle, string body, int authorId, int categoryId, DateTime dateCreated, DateTime lastUpdated, int numberOfLikes, int numberOfShares)
        {
            Title = title;
            Subtitle = subtitle;
            Body = body;
            AuthorId = authorId;
            DateCreated = dateCreated;
            LastUpdated = lastUpdated;
            NumberOfLikes = numberOfLikes;
            NumberOfShares = numberOfShares;
            CategoryId = categoryId;
        }

        public static Article CreateArticle(string title, string subtitle, string body, int authorId, int numberOfLikes, int numberOfShares)
        {
            return new Article
            {
                Title = title,
                Subtitle = subtitle,
                Body = body,
                AuthorId = authorId,
                NumberOfLikes = numberOfLikes,
                NumberOfShares = numberOfShares,
                DateCreated = DateTime.Now
            };
        }
        public void UpdateArticleInfo(string title, string subtitle, string body, int numberOfLikes, int numberOfShares)
        {
            Title = title;
            Subtitle = subtitle;
            Body = body;
            NumberOfLikes = numberOfLikes;
            NumberOfShares = numberOfShares;
            LastUpdated = DateTime.Now;
        }
        public void SetAuthor(Author author)
        {
            Author = author;
        }
    }
}
