using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crudops_Console_Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crudops_Console_Application.Tests
{
    [TestClass()]
    public class HttpGetRequestTests
    {
        [TestMethod()]  // // This checks that the URL is valid with a successful internet connection.
        public async Task CheckSuccessfulResponceTest() 
        {
            // arrange
            HttpGetRequest request = new HttpGetRequest();
            int retryCounter = 0;
            bool success = false;

            // act
            await request.GetRequest("https://herrcomockapifunc.azurewebsites.net//api//getProducts");

            while(!request.response.IsSuccessStatusCode && retryCounter < 10 )
            {
                Thread.Sleep(4000);
                await request.GetRequest("https://herrcomockapifunc.azurewebsites.net//api//getProducts");
                retryCounter++;
            }

            if(request.Message.Equals("") && request.response.IsSuccessStatusCode)
            {
                success = true;
            }

            // assert
            Assert.AreEqual(success, true);
        }

        
        
    }
}