using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using myMicroservice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
namespace myMicroservice.Controllers {
    [Route ("api/[controller]")]

    public class GoogleApiController : ControllerBase {
        private string google_key { get; }
        public GoogleApiController (IConfiguration configuration) {
            google_key = configuration["google_key"];
        }

        // GET api/googleapi
        [HttpGet]
        [Route("GetRestaurants/{query}")]
        [ResponseCache (CacheProfileName = "Default30")] //Use CacheProfile
        public async Task<GoogleApiResponse> Get (string query = "Bangsue") //set default query
        {
            using (var client = new HttpClient ()) {
                string Url = $"https://maps.googleapis.com/maps/api/place/textsearch/json?query=restaurants+in+{query}&key={google_key}";
                var response = await client.GetStringAsync (Url);
                var result = JsonConvert.DeserializeObject<GoogleApiResponse> (response);
                return result;
            }
        }

    }
}