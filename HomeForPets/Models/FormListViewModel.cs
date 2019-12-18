using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using ModelDB;
using HomeForPets.Infrastructure;

namespace HomeForPets.Models
{
    public class FormListViewModel : IFormView
    {
        public IEnumerable<Form> Forms { get; set; }
        [Display(Name = "Категория")]
        public SelectList Categories { get; set; }
        [Display(Name = "Порода")]
        public SelectList Species { get; set; }
    }
}
