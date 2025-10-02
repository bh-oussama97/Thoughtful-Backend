using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thoughtful.Dal.Email
{
    public class EmailModel
    {
        public List<string> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        private DateTime _creationTime { get; set; } = DateTime.Now;
        public DateTime CreationTime { get { return this._creationTime; } }
    }
}
