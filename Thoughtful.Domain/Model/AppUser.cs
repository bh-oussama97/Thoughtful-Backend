
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thoughtful.Domain.Model
{
    [Table("Users")]
    public class AppUser : IdentityUser
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ICollection<BlogContributor> BlogContributions { get; set; }
    }
}
