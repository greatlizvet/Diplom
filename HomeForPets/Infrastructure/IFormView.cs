using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeForPets.Infrastructure
{
    public interface IFormView
    {
        SelectList Categories { get; set; }
        SelectList Species { get; set; }
        SelectList Cities { get; set; }
    }
}
