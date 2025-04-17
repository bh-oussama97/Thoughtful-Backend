namespace Thoughtful.Domain.Model
{
    public class Category
    {
        public Category()
        {
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Article> Articles { get; set; }


        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
