using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelDB;

namespace HomeForPets.Models
{
    public class OrderForRegistrationViewModel
    {
        public OrderForRegistration OrderForRegistration { get; set; }
        
        [Display(Name = "Город")]
        public SelectList Cities { get; set; }
    }
}