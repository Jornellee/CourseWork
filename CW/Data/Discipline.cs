﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data
{
    class Discipline
    {
        public int id { get; set; }
        public string name { get; set; }

        public Discipline(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Discipline()
        {

        }
    }
}
