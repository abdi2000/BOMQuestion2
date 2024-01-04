using System.Text.Json.Serialization;

namespace BOMQuestion2
{
    public class WeatherDataResponse
    {
        public List<Data> WeatherResponse { get; set; }
    }

    [Serializable]
    public class WeatherDataFilteredResponse
    {
        public string Element { get; set; }
        public object Value { get; set; }
    }
}
