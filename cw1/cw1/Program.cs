using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace cw1
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            {
                Console.WriteLine("Start");
                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(args[0]);

                if (response.IsSuccessStatusCode)
                {
                    var html = await response.Content.ReadAsStringAsync();

                    var regex = new Regex("[a-z0-9]+@[a-z.]+");

                    var matches = regex.Matches(html);

                    foreach (var i in matches)
                    {
                        Console.WriteLine(i);
                    }
                }
                Console.WriteLine("Koniec");
            }
        }
    }
}
