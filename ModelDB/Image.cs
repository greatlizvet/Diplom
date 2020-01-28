using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDB
{
    public class Image
    {
        public int ImageID { get; set; }
        public string Path { get; set; }

        public virtual ICollection<Form> Forms { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }

        public Image()
        {
            Forms = new List<Form>();
            AppUsers = new List<AppUser>();
        }
    }
}
