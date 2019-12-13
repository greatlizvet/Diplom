using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.IO;

namespace ModelDB
{
    public class FormCreateViewModel
    {
        public Form Form { get; set; }
        [Display(Name = "Категория")]
        public SelectList Categories { get; set; }
        [Display(Name = "Порода")]
        public SelectList Specie { get; set; }
    }
}
