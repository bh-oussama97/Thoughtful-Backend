using Thoughtful.Api.Features.AuthorFeature;
using Thoughtful.Api.Features.Categories.Dto;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Articles.DTO
{
    public class ArticleGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Subtitle { get; set; }
        public string Body { get; set; }
        public int AuthorId { get; set; }
        public AuthorGetDto Author { get; set; }
        public CategoryGetDto Category { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public int NumberOfLikes { get; set; }
        public int NumberOfShares { get; set; }
        public ArticleGetDto()
        {

        }
        public ArticleGetDto(Article article)
        {
            Id = article.Id;
            Title = article.Title;
            Subtitle = article.Subtitle;
            Body = article.Body;
            AuthorId = article.AuthorId;
            DateCreated = article.DateCreated;
            LastUpdated = article.LastUpdated;
            NumberOfLikes = article.NumberOfLikes;
            NumberOfShares = article.NumberOfShares;

        }
    }

}
