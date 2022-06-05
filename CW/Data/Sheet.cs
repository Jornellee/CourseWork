using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data
{
    class Sheet
    {
        public int id { get; set; }
        public string type { get; set; }
        public Group group { get; set; }
        public Discipline discipline { get; set; }
        public Institute institute { get; set; }

        public Sheet(int id, string type, Group group, Discipline discipline, Institute institute)
        {
            this.id = id;
            this.type = type;
            this.group = group;
            this.discipline = discipline;
            this.institute = institute;
        }

        public Sheet()
        {

        }
    }
}
