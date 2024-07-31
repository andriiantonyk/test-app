using LogSearchConsole.Services;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

string directoryPath = configuration["FileDirectory"];
string pattern = configuration["SearchPattern"];

var service = new LogSearchService(directoryPath);
var found = service.SearchLogs(pattern); 

foreach(var i in found)
{
    Console.WriteLine(i);
}
