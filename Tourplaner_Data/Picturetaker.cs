using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tourplaner_Data
{
    public class Picturetaker
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<string> GetPictruce(string from, string to)
        {
            string responseBody = String.Empty;
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://www.mapquestapi.com/directions/v2/route?key=wJH0FXZIHnP3ttxaM8qswamKylCJH1A3&from="+from+ "&to="+to );
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                Console.WriteLine(responseBody);
                
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return responseBody;
        }
    }
}
