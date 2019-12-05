using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelDB
{
    public class Specie
    {
        public int SpecieID { get; set; }
        public string SpecieName { get; set; }

        public virtual ICollection<Form> Forms { get; set; }

        public Specie()
        {
            Forms = new List<Form>();
        }
    }
}
