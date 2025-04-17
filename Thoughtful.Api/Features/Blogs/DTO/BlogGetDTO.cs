using Thoughtful.Api.Features.AuthorFeature;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Blogs.DTO
{
    public class BlogGetDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime DateCreated { get; init; }
        public ICollection<AuthorGetDto> Contributors { get; set; }
        public BlogGetDTO(Blog blog)
        {
            Id = blog.Id;
            Name = blog.Name;
            Description = blog.Description;
            DateCreated = blog.CreatedDate;
        }

    }
}
