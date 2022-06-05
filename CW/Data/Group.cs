using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data
{
    class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public Institute institute { get; set; }

        public Group(int id, string name, Institute InsId)
        {
            this.id = id;
            this.name = name;
            this.institute = institute;
        }

        public Group()
        {

        }
    }
}
