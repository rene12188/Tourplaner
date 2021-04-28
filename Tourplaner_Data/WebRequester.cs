using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Tourplaner_Utility;

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
                    $"https://open.mapquestapi.com/staticmap/v5/map?start={from}&end={to}&size=@2x&key="+ CFGManager.ReadSetting("MapKey"));
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

        public static async Task<string> GetJson(string from, string to)
        {
            string responseBody = null;
            try
            {

                HttpResponseMessage response = await client.GetAsync(
                    $"http://open.mapquestapi.com/directions/v2/route?key="+ CFGManager.ReadSetting("MapKey") + $"&from={from}&to={to}");
                response.EnsureSuccessStatusCode();
                responseBody = response.Content.ReadAsStringAsync().Result;

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
            return responseBody;
        }

        public static async Task<float> ReturnDistance(string from, string to)
        {
            Task<string> tmp = GetJson(from, to);
            await tmp;

            JObject jsonObject = JObject.Parse(tmp.Result);
            return (float)jsonObject["route"]?["distance"];
        }
    }
}
