using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using SkyApp.Data.Weather;

namespace SkyApp.Web.Weather;

public class WebClient:
    IWeatherApi
{
    private readonly HttpClient _client;

    public WebClient()
    {
        _client = new();
        _client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "Test");
        _client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");
    }


    public async Task<WeatherDto> GetWeather()
    {


        using HttpRequestMessage request = new()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri("https://weatherapi-com.p.rapidapi.com/current.json")
        };
        

        return null;
    }

    public async Task<WeatherDto> GetWeather(string location)
    {
        throw new System.NotImplementedException();
    }
}