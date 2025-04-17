using Thoughtful.Domain.Model;

namespace Thoughtful.Dal
{
    public class Seed
    {
        public static async Task SeedData(ThoughtfulDbContext context)
        {
            //var blogs = new List<Blog>
            //    {
            //        new Blog( "The Future of Web Design", "Exploring modern design trends and techniques shaping the web.",DateTime.Now),
            //        new Blog( "AI-Powered Content Creation", "How artificial intelligence is revolutionizing content generation.",DateTime.Now),
            //        new Blog( "The Role of UX in Digital Products", "Understanding the importance of user experience in product design.",DateTime.Now),
            //        new Blog( "Cybersecurity in the Age of AI", "How AI-driven security measures are changing the cybersecurity landscape.",DateTime.Now),
            //        new Blog( "The Rise of Progressive Web Apps", "Why PWAs are becoming the future of mobile-friendly web applications.",DateTime.Now),
            //        new Blog( "Leveraging Data Science for Business Growth", "Using data analytics to drive business decision-making.",DateTime.Now),
            //        new Blog( "Cloud Computing: Benefits and Challenges", "A deep dive into cloud technology and its impact on businesses.",DateTime.Now),
            //        new Blog( "The Evolution of JavaScript Frameworks", "Comparing the latest trends in JavaScript frameworks for web development.",DateTime.Now),
            //        new Blog( "SEO Strategies for 2025", "How to optimize websites for better search engine rankings.",DateTime.Now),
            //        new Blog( "Blockchain Beyond Cryptocurrency", "Exploring real-world applications of blockchain technology.",DateTime.Now)
            //    };
            //var authors = new List<Author>
            //    {
            //        new Author("Sophia", "Martinez", "Front-end developer passionate about creating interactive user experiences.", DateTime.Parse("1993-11-30T11:20:00Z")),
            //        new Author( "James", "Anderson", "DevOps engineer streamlining CI/CD pipelines and cloud infrastructure.", DateTime.Parse("1987-06-18T15:45:00Z")),
            //        new Author( "Olivia", "Taylor", "AI researcher specializing in natural language processing and robotics.", DateTime.Parse("1991-02-22T07:10:00Z")),
            //        new Author( "Liam", "Thomas", "Blockchain developer building decentralized applications and smart contracts.", DateTime.Parse("1994-08-09T13:30:00Z")),
            //        new Author( "Charlotte", "White", "Product manager leading cross-functional teams to deliver innovative solutions.", DateTime.Parse("1989-05-04T10:55:00Z"))
            //    };

            //var categories = new List<Category>
            //    {
            //        new Category("Technology", "Articles and insights on the latest technology trends and innovations."),
            //        new Category("Design", "Exploring the world of design, including UI/UX, branding, and creativity."),
            //        new Category("Data Science", "Discussions on machine learning, AI, big data, and analytics."),
            //        new Category("Marketing", "Strategies and trends in digital marketing, SEO, and social media."),
            //        new Category("Cybersecurity", "Guides and best practices for securing digital systems and data."),
            //        new Category("Artificial Intelligence", "Exploring the advancements and applications of AI in various industries."),
            //        new Category("Software Development", "Best practices, tutorials, and discussions on coding and software engineering."),
            //        new Category("Blockchain", "Insights into decentralized technologies, cryptocurrencies, and smart contracts."),
            //        new Category("Business", "Entrepreneurship, management, and strategies for business success."),
            //        new Category("Health & Wellness", "Tips and information on fitness, mental health, and well-being.")
            //    };

            //await context.Authors.AddRangeAsync(authors);
            //await context.Categories.AddRangeAsync(categories);
            //await context.Blogs.AddRangeAsync(blogs);
            //await context.SaveChangesAsync(); // Save to assign IDs

            Category techCategory = context.Categories.First(c => c.Name.ToLower() == "technology");
            Category designCategory = context.Categories.First(c => c.Name.ToLower() == "design");
            Category cybersecurityCategory = context.Categories.First(c => c.Name.ToLower() == "cybersecurity");
            Category blockchainCategory = context.Categories.First(c => c.Name.ToLower() == "blockchain");
            Category businessCategory = context.Categories.First(c => c.Name.ToLower() == "business");

            Author sophia = context.Authors.First(c => c.FirstName.ToLower() == "sophia");
            Author James = context.Authors.First(c => c.FirstName.ToLower() == "james");
            Author Olivia = context.Authors.First(c => c.FirstName.ToLower() == "olivia");
            Author Liam = context.Authors.First(c => c.FirstName.ToLower() == "liam");
            Author Charlotte = context.Authors.First(c => c.FirstName.ToLower() == "charlotte");

            var articles = new List<Article>
            {
                new Article
                (
                    "The Future of Artificial Intelligence",
                    "AI in Healthcare and Finance",
                  "AI is revolutionizing healthcare and finance by providing more accurate predictions and automating routine tasks...",
                     sophia.Id,
                     techCategory.Id,
                   DateTime.Parse("2025-03-05T14:12:31.883Z"),
                     DateTime.Parse("2025-03-05T14:12:31.883Z"),
                   10,
                     5
                ),
                new Article
                 (
                    "UI/UX Design for Beginners",
                     "A Guide to Crafting User-Friendly Interfaces",
                     "UI/UX design plays a crucial role in making websites and apps more accessible and user-friendly...",
                     James.Id,
                     designCategory.Id,
                    DateTime.Parse("2025-03-05T14:12:31.883Z"),
                     DateTime.Parse("2025-03-05T14:12:31.883Z"),
                     20,
                    15
                 ),
                new Article
                (
                    "Big Data: Transforming Business Decisions",
                     "Leveraging Data to Drive Business Insights",
                     "Big data analytics is now a critical tool for companies seeking to make data-driven decisions...",
                     Olivia.Id,
                     cybersecurityCategory.Id,
                    DateTime.Parse("2025-03-05T14:12:31.883Z"),
                     DateTime.Parse("2025-03-05T14:12:31.883Z"),
                     8,
                     3
                ),
                new Article
                (
                    "Mastering SEO in 2025",
                     "How to Rank Higher and Drive Traffic",
                     "Search engine optimization is constantly evolving. Here’s how to optimize your site for the latest trends...",
                     Liam.Id,
                     blockchainCategory.Id,
                    DateTime.Parse("2025-03-05T14:12:31.883Z"),
                    DateTime.Parse("2025-03-05T14:12:31.883Z"),
                     12,
                    7
                ),
                new Article
                (
                    "Best Practices for Cybersecurity in 2025",
                     "Securing Your Data from Emerging Threats",
                     "As cyber threats become more sophisticated, protecting sensitive data is more critical than ever...",
                     Charlotte.Id,
                     businessCategory.Id,
                    DateTime.Parse("2025-03-05T14:12:31.883Z"),
                     DateTime.Parse("2025-03-05T14:12:31.883Z"),
                     15,
                    10
                )
            };


            // Now add articles
            await context.Articles.AddRangeAsync(articles);
            await context.SaveChangesAsync();
        }
    }
}
