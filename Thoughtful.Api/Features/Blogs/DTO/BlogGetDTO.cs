using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Blogs.DTO
{
    public class BlogGetDTO
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public DateTime CreatedDate { get; init; }
        public ICollection<BlogContributorDto> Contributors { get; set; }
        public BlogGetDTO()
        {

        }
        public BlogGetDTO(Blog blog)
        {
            Id = blog.Id;
            Name = blog.Name;
            Description = blog.Description;
            CreatedDate = blog.CreatedDate;
        }

    }
}
