using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace Crudops_Console_Application
{
    public class SheetGenerator
    {

        public SheetGenerator(string content, Sheet sheet)
        {
            checkValidJSON(content);
            assembleItems(content);
            sheet.Rows = Rows;
            sheet.Columns = Columns;

        }


        public void assembleItems(string contents)
        {
            // if the content from the GET request is valid JSON format, proceed with assembling items and generating the sheet
            if (ValidJSON == true)
            {
                // Parsing the JSON string into a C# object, collecting all the data found in rows -> cells
                var cells = JObject.Parse(contents)["rows"].SelectMany(r => r["cells"]);

                // Create three lists, with the column number, row number and the values. They will be used to create the sheet
                List<int> columnIds = cells.Select(c => (int)c["columnId"]).ToList();
                List<int> rowIds = cells.Select(c => (int)c["rowId"]).ToList();
                List<string> values = cells.Select(c => (string)c["value"]).ToList();

                // Now parsing the data in 'columns' This will be used to collect the column names
                var columns = JObject.Parse(contents)["columns"];
                List<string> columnNames = columns.Select(c => (string)c["name"]).ToList();

                GenerateSheetRows(rowIds, columnIds, values, columnNames.Count);
                GenerateSheetColumns(columnNames);

            }
            else
            {
                Console.WriteLine("The contents are not valid JSON format, Exiting program");
                Environment.Exit(0);
            }

        }

        public void GenerateSheetColumns(List<string> colNames)
        {
            Columns = new List<Column>();

            // This will create column object, assigning id and name, then storing in the 'Columns' list.
            for (int i = 0; i < colNames.Count; i++)
            {
                Column column = new Column(i, colNames[i]);
                Columns.Add(column);
            }

        }

        public void GenerateSheetRows(List<int> rowIds, List<int> colIds, List<string> vals, int numberOfCols)
        {
            Rows = new List<Row>();
            List<Cell> cells = new List<Cell>();

            for (int i = 0; i < rowIds.Count; i++)
            {
                // Creating a cell object, which is assigned a column number, row number and a value
                Cell cell = new Cell(colIds[i], rowIds[i], vals[i]);

                // If we are on the start of a new row, remove all the previous values (i.e from previous row) in 'cells'
                if (colIds[i] == 0)
                {
                    cells.RemoveRange(0, cells.Count);
                    cells.Add(cell);                  
                }
                else
                {
                    cells.Add(cell);
                }

                // If on last column, create a copy of the 'cells' list, create a row object and store it in Rows list
                if (colIds[i] == numberOfCols - 1) 
                {   
                    List<Cell> tempCells = new List<Cell>(cells);
                    Row row = new Row(rowIds[i], tempCells);
                    Rows.Add(row);
                }


            }

        }


        public bool checkValidJSON(string contents)
        {
         
                JsonSchema schema = JsonSchema.Parse(contents);
                JObject jObject = JObject.Parse(@"{'id': 2,'name': 'aSheet'  }");
                ValidJSON = jObject.IsValid(schema);
                return ValidJSON;
        }


        public List<Column> Columns { get; set; }
        public List<Row> Rows { get; set; }

        public bool ValidJSON { get; set; }



    }
}
