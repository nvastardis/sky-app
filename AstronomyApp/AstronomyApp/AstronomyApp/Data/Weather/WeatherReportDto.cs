using System;

namespace AstronomyApp.Data.Weather;

public class WeatherReportDto
{
    public DateTime Last_updated { get; set; }
    public double Temp_c { get; set; }
    public double Temp_f { get; set; }
    public int Is_day { get; set; }
    public ConditionsDto Conditions { get; set; }
    public double Wind_mph { get; set; }
    public double Wind_kph { get; set; }
    public double Wind_degree { get; set; }
    public double Wind_dir { get; set; }
    public double Pressure_mb { get; set; }
    public double Pressure_in { get; set; }
    public double Precip_mm { get; set; }
    public double Precip_in { get; set; }
    public int Humidity { get; set; }
    public int Cloud { get; set; }
    public double Feelslike_c { get; set; }
    public double Feelslike_f { get; set; }
    public double Vis_km { get; set; }
    public double Vis_miles { get; set; }
    public int Uv { get; set; }
    public double Gust_mph { get; set; }
    public double Gust_kph { get; set; }
}