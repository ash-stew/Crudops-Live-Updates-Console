# Crudops-Live-Updates-Console
Console Application for Crudops Live Updates

This application makes a call to an API, extracts the data and stores into a sheet. The process repeats every 8 seconds for 15 iterations.
Information will be provided on the number of created, deleted and updated rows per iteration.

RUNNING THE PROJECT:

Go to code -> 'Open with Visual Studio'  This will clone the items into a folder on your local directory.
Go to the local folder and go into Crudops_Console_Application and open 'Crudops_Console_ApplicationTests.csproj' with Visual Studio 2022.

INCLUDING AND RUNNING THE TESTS:
To include the HttpGetRequestTests.cs and SheetGeneratorTests.cs
go into the HttpGetRequest.cs code and right click, select 'Create Unit Tests' and click OK.
Once created, remove the code and copy and paste the code from the HttpGetRequestTests.cs.  
Then go into SheetGenerator.cs and repeat the process above, copying the code from the SheetGeneratorTests.cs

IMPORTANT- When running the application, please ensure that you are connected to the internet.
