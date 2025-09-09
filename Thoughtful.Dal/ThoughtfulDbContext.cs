using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Thoughtful.Domain.Model;

namespace Thoughtful.Dal
{
    public class ThoughtfulDbContext : IdentityDbContext<AppUser, Role, string,
                                        IdentityUserClaim<string>, ApplicationUserRole,
                                        IdentityUserLogin<string>, IdentityRoleClaim<string>,
                                        IdentityUserToken<string>>
    {
        public ThoughtfulDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            modelBuilder.Entity<ApplicationUserRole>(entity =>
            {
                entity.ToTable(name: "UsersRoles");
            });
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });
            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable(name: "Roles");

            });

            // one-to-many: one category -> many articles
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Articles)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Author>()
                 .ToTable("Authors")
                 .HasKey(t => t.Id);

            modelBuilder.Entity<UserProfilePhoto>()
             .ToTable("ProfilePhotos")
             .HasKey(t => t.Id);

            modelBuilder.Entity<Article>()
                 .ToTable("Articles")
                 .HasKey(t => t.Id);

            modelBuilder.Entity<Article>()
                .HasOne(a => a.Author)
                .WithMany(b => b.Articles)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<BlogContributor>()
       .HasKey(bc => new { bc.BlogId, bc.UserId });

            modelBuilder.Entity<BlogContributor>()
                .HasOne(bc => bc.Blog)
                .WithMany(b => b.Contributors)
                .HasForeignKey(bc => bc.BlogId);

            modelBuilder.Entity<BlogContributor>()
                .HasOne(bc => bc.User)
                .WithMany(u => u.BlogContributions)
                .HasForeignKey(bc => bc.UserId);


            modelBuilder.Entity<UserProfilePhoto>()
                .HasOne(bc => bc.User)
                .WithMany(u => u.UserPhotos)
                .HasForeignKey(bc => bc.UserId);

        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ApplicationUserRole> UserRoles { get; set; }
        public DbSet<UserProfilePhoto> UserProfilePhotos { get; set; }

    }
}
