public class Data
{
    public int sort_order { get; set; }
    public int wmo { get; set; }
    public string name { get; set; }
    public string history_product { get; set; }
    public string local_date_time { get; set; }
    public string local_date_time_full { get; set; }
    public string aifstime_utc { get; set; }
    public double lat { get; set; }
    public double lon { get; set; }
    public double apparent_t { get; set; }
    public string cloud { get; set; }
    public int? cloud_base_m { get; set; }
    public int? cloud_oktas { get; set; }
    public int? cloud_type_id { get; set; }
    public string cloud_type { get; set; }
    public double delta_t { get; set; }
    public int gust_kmh { get; set; }
    public int gust_kt { get; set; }
    public double air_temp { get; set; }
    public double dewpt { get; set; }
    public double press { get; set; }
    public double press_qnh { get; set; }
    public double press_msl { get; set; }
    public string press_tend { get; set; }
    public string rain_trace { get; set; }
    public int rel_hum { get; set; }
    public string sea_state { get; set; }
    public string swell_dir_worded { get; set; }
    public object swell_height { get; set; }
    public object swell_period { get; set; }
    public string vis_km { get; set; }
    public string weather { get; set; }
    public string wind_dir { get; set; }
    public int wind_spd_kmh { get; set; }
    public int wind_spd_kt { get; set; }
}

public class Header
{
    public string refresh_message { get; set; }
    public string ID { get; set; }
    public string main_ID { get; set; }
    public string name { get; set; }
    public string state_time_zone { get; set; }
    public string time_zone { get; set; }
    public string product_name { get; set; }
    public string state { get; set; }
}

public class Notice
{
    public string copyright { get; set; }
    public string copyright_url { get; set; }
    public string disclaimer_url { get; set; }
    public string feedback_url { get; set; }
}

public class Observations
{
    public List<Notice> notice { get; set; }
    public List<Header> header { get; set; }
    public List<Data> data { get; set; }
}

public class WeatherData
{
    public Observations observations { get; set; }
}

