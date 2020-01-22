using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ModelDB
{
    public class AppRole : IdentityRole
    {
        public AppRole() : base()
        { }

        public AppRole(string roleName) : base(roleName)
        { }
    }
}
