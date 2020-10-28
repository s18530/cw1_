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
            Console.WriteLine("Podaj adres URL (  https://www.  )");
               string url = Console.ReadLine();
               var urlRegex = new Regex(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)");
               var urlMatches = urlRegex.Matches(url ?? throw new ArgumentNullException());
               var httpClient = new HttpClient();
               var response = await httpClient.GetAsync(url);
               
               if (urlMatches.Count == 0)
               {
                   throw new ArgumentException();
               }
               
               if (response.IsSuccessStatusCode)
               {
                   var html = await response.Content.ReadAsStringAsync();
                   var emailRegex = new Regex("[a-z0-9]+@[a-z.]+");
                   var emailMatches = emailRegex.Matches(html);
                   var uniqeEmailMatches = emailMatches.OfType<Match>().Select(m => m.Value).Distinct();
                   if (emailMatches.Count == 0)
                   {
                       Console.WriteLine("Nie znaleziono adresow email");
                   }else
                   {
                       foreach (var i in uniqeEmailMatches.ToList())
                       {
                           Console.WriteLine(i);
                       }
                   }
               }
               else
               {
                   Console.WriteLine("Blad w czasie pobierania strony");
               }
               httpClient.Dispose();
               Console.WriteLine("Koniec");
        }
    }
}