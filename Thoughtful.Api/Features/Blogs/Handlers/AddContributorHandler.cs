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
            string FileName = "";
            var blog = await _ctx.Blogs
                    .Include(b => b.Contributors)
                    .FirstOrDefaultAsync(b => b.Id == request.Contribution.BlogId, cancellationToken);

            var user = await _ctx.Users
                .FirstOrDefaultAsync(u => u.Id == request.Contribution.ContributorId, cancellationToken);

            if (blog == null || user == null)
            {
                return await Task.FromResult(Result<string>.Failure(new Error($"Invalid blog or contributor ID.", "InvalidBlog")));

            }

            if (blog.Contributors.Any(c => c.UserId == user.Id))
            {

                return await Task.FromResult(Result<string>.Failure(new Error($"Contributor {user.UserName} is already added to the blog {blog.Id}", "ContributionAlreadyExists")));
            }


            if (request.Contribution.File != null && request.Contribution.File.Length > 0)
            {

                // Create the directory if it does not exist.
                if (!Directory.Exists(_uploadRoot))
                {
                    Directory.CreateDirectory(_uploadRoot);
                }
                FileName = Path.GetFileName(request.Contribution.File.FileName);

                // Save the uploaded file to the server.
                string strFilePath = _uploadRoot + FileName;
                if (File.Exists(strFilePath))
                {

                    return await Task.FromResult(Result<string>.Failure(new Error($" {FileName} already exists on the server!", "FileAlreadyExists")));

                }
                else
                {
                    using (var stream = System.IO.File.Create(strFilePath))
                    {
                        await request.Contribution.File.CopyToAsync(stream);
                    }
                }
            }

            blog.Contributors.Add(new BlogContributor
            {
                BlogId = blog.Id,
                UserId = user.Id,
                Note = request.Contribution.Note,
                Filename = FileName,
                ContributionDate = DateTime.UtcNow
            });

            int resultSaving = await _ctx.SaveChangesAsync(cancellationToken);
            if (resultSaving > 0)
            {
                return await Task.FromResult(Result<string>.Success($"Contributor with Id {user.Id} added successfully."));
            }

            return result;
        }
    }
}
