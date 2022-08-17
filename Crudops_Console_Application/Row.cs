using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crudops_Console_Application
{
    public class Row
    {
        public Row(int id, List<Cell> cells)
        {
            Id = id;
            Cells = cells;
        }
        public int Id { get; set; }
        public List<Cell> Cells { get; set; }


    }
}
