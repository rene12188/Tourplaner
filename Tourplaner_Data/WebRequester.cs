using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tourplaner_Data
{
    public class WebRequester
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<Byte[]> GetPicture(string from, string to)
        {
            Byte[] responseBody = null;
            try
            {

                HttpResponseMessage response = await client.GetAsync(
                    "https://open.mapquestapi.com/staticmap/v5/map?start="+from+"&end="+to+"&size=@2x&key=wJH0FXZIHnP3ttxaM8qswamKylCJH1A3");
                //response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsByteArrayAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
               
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
