using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crudops_Console_Application
{
    public class SheetCopy
    {
        public SheetCopy(Sheet oldSheet, Sheet newSheet)
        {
            oldSheet.Rows = new List<Row>(newSheet.Rows);
            oldSheet.Columns = new List<Column>(newSheet.Columns);
        }

    }
}
