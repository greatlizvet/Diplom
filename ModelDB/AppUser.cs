using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ModelDB
{
    public class AppUser : IdentityUser
    {
        public int? ImageID { get; set; }
        public virtual Image Image { get; set; }

        public string TitleName { get; set; }

        [Required]
        public int CityID { get; set; }
        public virtual City City { get; set; }

        public virtual ICollection<Form> Forms { get; set; }

        public AppUser()
        {
            Forms = new List<Form>();
        }
    }
}
