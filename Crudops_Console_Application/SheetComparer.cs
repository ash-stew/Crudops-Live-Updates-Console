using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crudops_Console_Application
{
    public class SheetComparer
    {
        public SheetComparer(Sheet originalSheet, Sheet newSheet)
        {
            CreatedRows = new List<Row>();
            DeletedRows = new List<Row>();
            UpdatedRows = new List<string>();
            FindCreatedAndUpdatedRows(originalSheet, newSheet);
            FindDeletedRows(originalSheet, newSheet);

        }

        public List<Row> CreatedRows { get; set; }
        public List<Row> DeletedRows { get; set; }

        public List<string> UpdatedRows { get; set; }

        public void FindCreatedAndUpdatedRows(Sheet oldSheet, Sheet newSheet)
        {


            for (int i = 0; i < newSheet.Rows.Count; i++)
            {
                bool found = false;
                // Each value from newSheet will be compared to all the values in oldSheet
                for (int j = 0; j < oldSheet.Rows.Count; j++)
                {
                    // If the id in the new sheet exists in previous sheet
                    if (newSheet.Rows[i].Cells[0].Value.Equals(oldSheet.Rows[j].Cells[0].Value))
                    {
                        // check to see if any of the values have changed in that row
                        SearchUpdatedValues(newSheet.Rows[i], oldSheet.Rows[j]);
                        found = true;
                        break;
                    }

                }
                // If the id has not been found in the old sheet, then it must have been created
                if (found == false) 
                {
                    CreatedRows.Add(newSheet.Rows[i]);
                }

            }


        }

        public void FindDeletedRows(Sheet oldSheet, Sheet newSheet)
        {

            // Each value from oldSheet will be compared to all the values in newSheet
            for (int i = 0; i < oldSheet.Rows.Count; i++)
            {
                bool found = false;

                for (int j = 0; j < newSheet.Rows.Count; j++)
                {
                    // If the id in the new sheet has also been found in previous sheet
                    if (oldSheet.Rows[i].Cells[0].Value.Equals(newSheet.Rows[j].Cells[0].Value))
                    {
                        
                        found = true;
                        break;
                    }

                }

                if (found == false) // then the row must have been deleted
                {
                    DeletedRows.Add(oldSheet.Rows[i]);
                }

            }


        }

        // Is called when a id in the previous sheet matches id in new sheet
        public void SearchUpdatedValues(Row oldRow, Row newRow)
        {
            bool updatedRowFound = false;
            string updateText = "";

            // This will check all of the values in both rows, (start from 1 as we know the id already matches)
            for (int i = 1; i < oldRow.Cells.Count; i++)
            {
                // if the values are not equal, then something has been modified
                if (!oldRow.Cells[i].Value.Equals(newRow.Cells[i].Value))
                {
                    // Creating a string representing information on new and old values                 
                    updateText += "ID: " + oldRow.Cells[0].Value + "     Old value:   " + oldRow.Cells[i].Value;
                    updateText += "     New value:   " + newRow.Cells[i].Value;
                    updatedRowFound = true;
                }


            }

            if (updatedRowFound == true)
            {
                UpdatedRows.Add(updateText);
            }


        }


    }
}
