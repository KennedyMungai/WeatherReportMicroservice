using System;
using System.Net.Http.Json;
using CloudWeather.DataLoader.Models;
using Microsoft.Extensions.Configuration;

internal class Program
{
    private static void Main(string[] args)
    {
        IConfiguration config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .AddEnvironmentVariables()
                            .Build();

        var servicesConfig = config.GetSection("Services");

        var tempServiceConfig = servicesConfig.GetSection("Temperature");
        var tempServiceHost = tempServiceConfig["Host"];
        var tempServicePort = tempServiceConfig["Port"];

        var precipServiceConfig = servicesConfig.GetSection("Precipitation");
        var precipServiceHost = precipServiceConfig["Host"];
        var precipServicePort = precipServiceConfig["Port"];

        List<string> zipCodes = new()
        {
            "73026",
            "68104",
            "04401",
            "32808",
            "19717"
        };

        Console.WriteLine("Starting the data load");

        var temperatureHttpClient = new HttpClient();
        temperatureHttpClient.BaseAddress = new Uri($"http://{tempServiceHost}:{tempServicePort}");

        var precipitationHttpClient = new HttpClient();
        precipitationHttpClient.BaseAddress = new Uri($"http://{precipServiceHost}:{precipServicePort}");

        foreach (var zip in zipCodes)
        {
            Console.WriteLine($"Loading data for zip code {zip}");
            var from = DateTime.Now.AddYears(-2);
            var thru = DateTime.Now;

            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            {
                var temps = PostTemp(zip, day, temperatureHttpClient);
                PostPrecip(temps[0], zip, day, precipitationHttpClient);
            }
        }
    }

    private void PostPrecip(int lowTemp, string zip, DateTime day, HttpClient precipitationHttpClient)
    {
        Random rand = new();
        var isPrecip = rand.Next(2) < 1;

        PrecipitationModel precipitation;

        if (isPrecip)
        {
            var precipInches = rand.Next(1, 16);

            if (lowTemp < 32)
            {
                precipitation = new()
                {
                    AmountInches = precipInches,
                    WeatherType = "snow",
                    ZipCode = zip,
                    CreatedOn = day
                };
            }
            else
            {
                precipitation = new()
                {
                    AmountInches = precipInches,
                    WeatherType = "rain",
                    ZipCode = zip,
                    CreatedOn = day
                };
            }
        }
        else
        {
            precipitation = new()
            {
                AmountInches = 0,
                WeatherType = "none",
                ZipCode = zip,
                CreatedOn = day
            };
        }

        var precipResponse = precipitationHttpClient
                                .PostAsJsonAsync("observation", precipitation)
                                .Result;

        if (precipResponse.IsSuccessStatusCode)
        {
            Console.WriteLine(
                $"Posted Precipitation: Date: {day:d}" +
                $"Zip: {zip}" +
                $"Type: {precipitation.WeatherType}" +
                $"Amount (in.): {precipitation.AmountInches}"
            );
        }
    }

    private List<int> PostTemp(string zip, DateTime day, HttpClient temperatureHttpClient)
    {
        throw new NotImplementedException();
    }
}