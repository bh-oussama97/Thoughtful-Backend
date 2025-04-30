namespace Thoughtful.Domain.Model
{
    public class Blog
    {
        //protected Blog()
        //{
        //    Contributors = new List<Author>();
        //}
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateTime CreatedDate { get; set; }

        public ICollection<BlogContributor> Contributors { get; set; }
        //public int Id { get; private set; }
        //public string Name { get; private set; }
        //public string Description { get; private set; }
        //public DateTime CreatedDate { get; set; }

        //public ICollection<Author> Contributors { get; set; }

        public Blog(string name, string description, DateTime createdDate)
        {
            Name = name;
            Description = description;
            CreatedDate = createdDate;
        }
        public void UpdateBlogInfo(string name, string description)
        {
            Name = name;
            Description = description;
        }

        //public void SetOwner(Author author)
        //{
        //    Owner = author;
        //}

        //public void AddContributor(Author author)
        //{
        //    if (!(Contributors.Any(c => c.Id == author.Id)))
        //        Contributors.Add(author);
        //}

        //public void RemoveContributor(Author author)
        //{
        //    Contributors.Remove(author);
        //}
    }
}
