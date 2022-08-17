using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crudops_Console_Application
{
    public class Cell
    {
        public Cell(int columnId, int rowId, object value)
        {
            ColumnId = columnId;
            RowId = rowId;
            Value = value;
        }

        public int ColumnId { get; set; }
        public int RowId { get; set; }
        public object Value { get; set; }

    }
}
