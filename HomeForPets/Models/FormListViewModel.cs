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
    public enum SortType
    {
        [Display(Name = "Дата по возрастанию")]
        DateAsc,
        [Display(Name = "Дата по убыванию")]
        DateDesc
    }

    public class FormListViewModel : IFormView
    {
        public IEnumerable<Form> Forms { get; set; }

        [Display(Name = "Категория")]
        public SelectList Categories { get; set; }

        [Display(Name = "Порода")]
        public SelectList Species { get; set; }

        public int? CategoryID { get; set; }
        public int? SpecieID { get; set; }

        [Display(Name = "Название анкеты")]
        public string Search { get; set; }

        public string Sort { get; set; }

        public PageInfo PageInfo { get; set; }

        public FormListViewModel()
        {
            Sort = "desc";
        }
    }
}
