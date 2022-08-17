using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crudops_Console_Application
{
    public class Sheet
    {
        public Sheet(string name)
        {
            Id = 0;
            Name = name;
            Rows = new List<Row>();
            Columns = new List<Column>();

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Row> Rows { get; set; }
        public List<Column> Columns { get; set; }




    }
}
