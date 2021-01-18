using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public static class LucasHttpTrigger
    {
        [FunctionName("LucasHttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            int n = int.Parse(req.Query["n"]);

            return new OkObjectResult(JsonConvert.SerializeObject(new { result = Lucas(n) }));
        }        

        static int Lucas(int n)
        {
            if (n == 0)
                return 2;
            if (n == 1)
                return 1;
                
            return Lucas(n-1) + Lucas(n-2);
        }
    }
}
