using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class GeoCoordsService
    {
        private ILogger<GeoCoordsService> logger;
        private IConfigurationRoot _config;

        public GeoCoordsService(ILogger<GeoCoordsService> _logger, 
            IConfigurationRoot config)
        {
            logger = _logger;
            _config = config;
        }


        public async Task<GeoCoordsResult> GetCoordsAsync(string location) //we need to supply the name we want to get the coords for
        {
            var result = new GeoCoordsResult() //we set a default failed result so we can change it if we get the reliable coords
            {
                Success = false,
                Message = "Failed to get coordinates"
            };
        
            var apiKey = _config["Keys:BingKey"];
            var encodedName = WebUtility.UrlEncode(location);
            var url = $"http://dev.virtualearth.net/REST/v1/locations?q={encodedName}&key={apiKey}";

            // Read out the results

            
            // Fragile, might need to change if the Bing API changes

            var client = new HttpClient();

            var json = await client.GetStringAsync(url);    //here we get the results of the url query (above) for the name

            var results = JObject.Parse(json); //we take the created jason and parse it with linq to json. the Jobject is who parses it
            var resources = results["resourceSets"][0]["resources"];
            if (!resources.HasValues)
            {
                result.Message = $"Could not find '{location}' as a location";
            }
            else
            {
                var confidence = (string)resources[0]["confidence"];
                if (confidence != "High") //if we are not confident that geocoords are reliable, we will return an error
                {
                    result.Message = $"Could not find a confident match for '{location}' as a location";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"]; //if we get it we will get the coordinate
                    result.Latitude = (double)coords[0];
                    result.Longitude = (double)coords[1];
                    result.Success = true;
                    result.Message = "Success";
                }
            }

            return result;
        }
    }
}
