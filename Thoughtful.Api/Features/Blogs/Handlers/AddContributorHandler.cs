using Thoughtful.Api.Common;
using Thoughtful.Api.Features.Blogs.Commands;
using Thoughtful.Dal;
using Thoughtful.Domain.Model;

namespace Thoughtful.Api.Features.Blogs.Handlers
{
    public class AddContributorHandler : IRequestHandler<AddContributor, Result<string>>
    {
        private readonly ThoughtfulDbContext _ctx;

        public AddContributorHandler(ThoughtfulDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Result<string>> Handle(AddContributor request, CancellationToken cancellationToken)
        {
            var result = new Result<string>();
            string _uploadRoot = AppSettings.UploadFilePath;
            string fileName = "";

            var blog = await _ctx.Blogs
                    .Include(b => b.Contributors)
                    .FirstOrDefaultAsync(b => b.Id == request.Contribution.BlogId, cancellationToken);

            var user = await _ctx.Users
                .FirstOrDefaultAsync(u => u.Id == request.Contribution.ContributorId, cancellationToken);

            if (blog == null || user == null)
            {
                return Result<string>.Failure(new Error($"Invalid blog or contributor ID.", "InvalidBlog"));
            }

            if (blog.Contributors.Any(c => c.UserId == user.Id))
            {
                return Result<string>.Failure(new Error($"Contributor {user.UserName} is already added to the blog {blog.Id}", "ContributionAlreadyExists"));
            }

            if (request.Contribution.File.Length > 10 * 1024 * 1024) 
            {
                return Result<string>.Failure(new Error("File size exceeds 10MB limit", "FileTooLarge"));
            }
            if (request.Contribution.File != null && request.Contribution.File.Length > 0)
            {
                Directory.CreateDirectory(_uploadRoot);

                fileName = Path.GetFileNameWithoutExtension(request.Contribution.File.FileName)
                    .Replace(" ", "_")
                    .Replace("(", "")
                    .Replace(")", "")
                    + Path.GetExtension(request.Contribution.File.FileName);

                var filePath = Path.Combine(_uploadRoot, fileName);
                Console.WriteLine($"filePath :  {filePath}");

                Console.WriteLine($"Attempting to save file to: {filePath}");


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Contribution.File.CopyToAsync(stream);
                }

                if (!File.Exists(filePath))
                {
                    return Result<string>.Failure(new Error("Failed to save file to disk", "FileSaveError"));
                }
            }
            string extension = Path.GetExtension(request.Contribution?.File?.FileName ?? "")
                      ?.TrimStart('.')
                      .ToLower();

            blog.Contributors.Add(new BlogContributor
            {
                BlogId = blog.Id,
                UserId = user.Id,
                Note = request.Contribution.Note,
                Filename = fileName,
                Extension = extension,
                ContributionDate = DateTime.UtcNow
            });

            await _ctx.SaveChangesAsync(cancellationToken);
            return Result<string>.Success($"Contributor with Id {user.Id} added successfully.");
        }
    }
}