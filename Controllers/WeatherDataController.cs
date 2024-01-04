using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BOMQuestion2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherDataController : ControllerBase
    {

        private readonly ILogger<WeatherDataController> _logger;

        public WeatherDataController(ILogger<WeatherDataController> logger)
        {
            _logger = logger;
        }

        [HttpGet("[action]/{station}")]
        public async Task<IActionResult> Get(string station)
        {
            try
            {
                int? wmo = GetWMO(station);
                _logger.Log(LogLevel.Information, $"The WMO is {wmo}");
                if (wmo == null) return BadRequest($"Incorrect WMO Input");
                var weatherData = await DeSerializeJsonData(wmo);
                return Ok(new WeatherDataResponse()
                {
                    WeatherResponse = weatherData.observations.data
                }

                    );
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Error getting weather from OpenWeather: {httpRequestException.Message}");
            }
        }
        [HttpGet("[action]/{station}/{element}")]
        public async Task<IActionResult> Get(string station, string element)
        {
            try
            {
                int? wmo = GetWMO(station);
                if (wmo == null) return BadRequest($"Incorrect WMO Input");

                var ele = typeof(Data).GetProperty(element);
                if(ele == null) return BadRequest($"Incorrect field Input");

                using (var client = new HttpClient())
                {
                    var json = await client.GetStringAsync($"http://www.bom.gov.au/fwo/IDS60901/IDS60901.{wmo}.json");
                    JObject parsedJson = JObject.Parse(json);
                    var result = parsedJson["observations"]["data"].Select(x => new WeatherDataFilteredResponse
                    {
                        Element = element,
                        Value =  x[element]
                    }).ToList();

                    var jsonResult = JsonConvert.SerializeObject(result,new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore});

                    return Ok(new
                    {
                        jsonResult
                    });
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Error getting weather information : {httpRequestException.Message}");
            }
        }

        private async Task<WeatherData> DeSerializeJsonData(int? wmo)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetFromJsonAsync<WeatherData>($"http://www.bom.gov.au/fwo/IDS60901/IDS60901.{wmo}.json");
                return response;
            }
        }
        private int? GetWMO(string station)
        {
            switch (station)
            {
                case "Adelaide Airport":
                    return 94672;
                case "Edinburgh":
                    return 95676;
                case "Hindmarsh Island":
                    return 94677;
                case "Kuitpo":
                    return 94683;
                default:
                    return null;
            }
        }
    }
}
