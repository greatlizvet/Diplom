using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.IO;
using ModelDB;
using HomeForPets.Infrastructure;

namespace HomeForPets.Models
{
    public class FormCreateViewModel : IFormView
    {
        public Form Form { get; set; }
        [Display(Name = "Категория")]
        public SelectList Categories { get; set; }
        [Display(Name = "Порода")]
        public SelectList Species { get; set; }
        [Display(Name = "Город")]
        public SelectList Cities { get; set; }
    }
}
