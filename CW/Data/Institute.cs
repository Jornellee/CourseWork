﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data
{
    class Institute
    {
        public int id { get; set; }
        public string name { get; set; }

        public Institute(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Institute()
        {

        }
    }
}
