using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crudops_Console_Application
{
    public class HttpGetRequest : HttpClient
    {
        public HttpGetRequest()
        {
            
        }

       
        public HttpResponseMessage response = new HttpResponseMessage();
        HttpClient client = new HttpClient();
        public List<string> ErrorLog = new List<string>();
        public string Message { get; set; }

        public string Content = "";

        public async Task GetRequest(string url)
        {
            Message = ""; // ensuring is set to "", this means everything is fine. 

            // this will attempt the Get Request and handle the possibility of user not having an internet connection
            try
            {
                response = await client.GetAsync(url);
                // response = await client.GetAsync("https://herrcomockapifunc.azurewebsites.net//api//getProducts");
            }
            catch (HttpRequestException ex)
            {
                Message = ex.Message;
                Console.WriteLine(ex.Message + " An internet connection is required, please ensure that you are connected");
            }


            if(response.IsSuccessStatusCode && Message.Equals("")) // Then everything is fine. Proceed with collecting the content
            {
                Content = await response.Content.ReadAsStringAsync();
            }

            else // either the was an error or the internet connection unsuccessful
            {
                if(!Message.Equals(""))
                {
                    ErrorLog.Add("Unsuccessful connection");
                }
                else
                {
                    ErrorLog.Add("Error " + (int)response.StatusCode + " " + response.StatusCode); // add the error to the error log
                }
            
            }
           
        }


    }
}
