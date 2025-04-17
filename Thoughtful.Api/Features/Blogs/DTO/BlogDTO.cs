namespace Thoughtful.Api.Features.Blogs.DTO
{
    public class BlogDTO
    {
        public string Name { get; init; }
        public string Description { get; init; }

        public BlogDTO() { }

        public BlogDTO(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
