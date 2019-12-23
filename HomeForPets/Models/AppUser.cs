using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelDB;

namespace HomeForPets.Models
{
    public class AppUser : IdentityUser
    {
        public int ImageID { get; set; }
        public Image Image { get; set; }
    }
}