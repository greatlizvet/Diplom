﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelDB;

namespace HomeForPets.Models
{
    public class RoleEditModel
    {
        public AppRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }
}