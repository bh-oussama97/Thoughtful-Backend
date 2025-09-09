using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thoughtful.Domain.Model
{
    public class UserProfilePhoto
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public string UserId { get; set; }                
        public AppUser User { get; set; } 

    }
}
