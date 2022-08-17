using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crudops_Console_Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Crudops_Console_Application.Tests
{
    [TestClass()]
    public class SheetGeneratorTests
    {
        

        [TestMethod()]
        public void checkValidJSONTest()
        {
            // Arrange
            HttpGetRequest request = new HttpGetRequest();
            request.GetRequest("https://herrcomockapifunc.azurewebsites.net//api//getProducts");
            int retryCounter = 0;

            // retry, if necessary
            while(request.Content.Equals("") && retryCounter < 10)
            {
                Thread.Sleep(4000);
                request.GetRequest("https://herrcomockapifunc.azurewebsites.net//api//getProducts");
                retryCounter++;
            }
          
            JsonSchema schema = JsonSchema.Parse(request.Content);
            JObject jObject = JObject.Parse(@"{
            'id': 2,
            'name': 'aSheet' 
             }");

            // Act
            bool valid = jObject.IsValid(schema);

            // Assert
            Assert.AreEqual(valid, true);
        }
    }
}