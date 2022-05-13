using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;


namespace TaskOnline.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class MealsController : ControllerBase
    {
        static readonly HttpClient client = new HttpClient();
        [HttpGet("/api/v1/meals/{name}")]
        public async Task<IActionResult> GetFood([FromRoute] string name)
        {
            string url = "https://www.themealdb.com/api/json/v1/1/search.php?s=Arrabiata" + name;
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return Ok(JsonConvert.DeserializeObject<MealsController>(responseBody));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return Ok(null);
        }
    }
}