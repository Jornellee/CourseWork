using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW.Data
{
    class Student
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string thName { get; set; }
        public Group group { get; set; }

        public Student(int id, string name, string surname, string thName, Group group)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.thName = thName;
            this.group = group;
        }

        public Student()
        {

        }
    }
}
