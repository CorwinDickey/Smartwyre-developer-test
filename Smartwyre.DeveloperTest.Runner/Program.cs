using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Text.Json;

namespace Smartwyre.DeveloperTest.Runner;

class Program
{
    static void Main(string[] args)
    {
        var rebateDataStore = new RebateDataStore();
        var productDataStore = new ProductDataStore();

        var rebateService = new RebateService(rebateDataStore, productDataStore);

        while (true)
        {
            Console.Write("Submit rebate calculation request: ");
            var inputString = Console.ReadLine();
            var inputObject = JsonSerializer.Deserialize<CalculateRebateRequest>(inputString);

            var result = rebateService.Calculate(inputObject);
            Console.WriteLine(JsonSerializer.Serialize(result));
            Console.WriteLine("");
        }
    }
}
