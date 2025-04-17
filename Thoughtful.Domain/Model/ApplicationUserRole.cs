using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Thoughtful.Domain.Model
{
    [Table("UsersRoles")]
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual AppUser User { get; set; }
        public virtual Role Role { get; set; }
    }
}
