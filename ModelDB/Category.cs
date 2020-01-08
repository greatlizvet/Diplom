using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDB
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<Specie> Species { get; set; }
        public virtual ICollection<Form> Forms { get; set; }

        public Category()
        {
            Forms = new List<Form>();
            Species = new List<Specie>();
        }
    }
}
