using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PLANNER.Models
{
    public static class CurrentExchangingRate
    {
        private static readonly string JSONPath = "https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json";
        private static List<ExchangingRateModel> ExchangingRates;
        public static async Task InitializeExangingRates()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(JSONPath);
                    response.EnsureSuccessStatusCode();
                    string currencySorce = await response.Content.ReadAsStringAsync();

                    ExchangingRates = JsonSerializer.Deserialize<List<ExchangingRateModel>>(currencySorce);
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine($"Request error: {e.Message}");
                }
            }
        }
        public static List<ExchangingRateModel> GetAllExchangingRates()=> ExchangingRates;
        public  static  ExchangingRateModel GetExhangingRateByCurrencyName(string currencyName)
        {
            return ExchangingRates.FirstOrDefault(rate => rate.cc.Equals(currencyName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
