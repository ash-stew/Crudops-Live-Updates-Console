using System.Threading;

namespace Crudops_Console_Application
{

    class Program
    {
        static async Task Main(string[] args)
        {
            HttpGetRequest request = new HttpGetRequest();
            Sheet originalSheet = new Sheet("name");
            Sheet newSheet = new Sheet("name");
            bool firstInstance = true;
            int counter = 0;

            while (counter < 15) 
            {
                if (firstInstance) // perform the main processes, but don't compare sheets, as there will not be anything to compare it to
                {
                    await PerformMainActivity(request, originalSheet);
                    firstInstance = false;
                }
                else  // perform the main processes, followed by comparing sheets, printing results
                {
                    await PerformMainActivity(request, newSheet);
                    SheetComparer sheetComparer = new SheetComparer(originalSheet, newSheet);
                    PrintResults(sheetComparer, request.ErrorLog);
                    request.ErrorLog.Clear();  // clearing the error log
                    SheetCopy sheetCopy = new SheetCopy(originalSheet, newSheet); // the newSheet now becomes the originalSheet
                }

                counter++;
                Thread.Sleep(8000); // pausing for 8 seconds
            }
            

           
        }

        static async Task PerformMainActivity(HttpGetRequest req, Sheet sheet)
        {
            string url = "https://herrcomockapifunc.azurewebsites.net//api//getProducts";
            int retryCounter = 1;
            await req.GetRequest(url);

            // if the previous attempt was unsuccessful, inform the user and keep trying until successful. Maximum of 10 attempts
            while (!req.response.IsSuccessStatusCode && retryCounter < 11) 
            {
                Console.WriteLine("Unsuccessful request {0}. Attempt number: {1} Retrying.... ", req.ErrorLog[0], retryCounter);
                Thread.Sleep(4000); // wait for 4 seconds before trying again
                await req.GetRequest(url);
                retryCounter++;
                
            }

            // if there is content available, then the GET request was successful- proceed with generating the sheet.
            if (req.Content != "") 
            {
                SheetGenerator generateSheet = new SheetGenerator(req.Content, sheet);
            }
            // there is no success code, no content and the max number of retry attempts has been reached. Inform the user and close down the program
            else
            {            
                    Console.WriteLine("Sorry. A successful GET request could not be established. Please try again later. Goodbye!");
                    Environment.Exit(0);            
            }

        }

        public static void PrintResults(SheetComparer comparer, List<string> errorLog)
        {
            // Providing information on the total number of each operation
            Console.WriteLine("\nNumber of Updates: " + comparer.UpdatedRows.Count);
            Console.WriteLine("Number of Deletes: " + comparer.DeletedRows.Count);
            Console.WriteLine("Number of Created: " + comparer.CreatedRows.Count);
            Console.WriteLine("Number of Errors:  " +  errorLog.Count);
            Console.WriteLine();

            // Printing the updated rows- old and new values
            foreach (string s in comparer.UpdatedRows) 
            {
                Console.WriteLine("UPDATED: " + s);
            }
                Console.WriteLine();

            // Printing the data of the deleted rows
            foreach (Row row in comparer.DeletedRows) 
            {
                Console.Write("DELETED:  ID: ", row.Id);
                foreach (Cell cell in row.Cells)
                {
                    Console.Write(" " + cell.Value);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            // Printing the rows that were created, along with the data
            foreach (Row row in comparer.CreatedRows)
            {

                Console.Write("CREATED:  ID: ", row.Id);
                foreach (Cell cell in row.Cells)
                {
                    Console.Write(" " + cell.Value);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            // Printing the errors, code along with description
            foreach (string s in errorLog)
            {
                Console.WriteLine(s);
            }

        }

     
    }



}