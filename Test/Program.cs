using System;
using System.Net;
using NeverNull;

namespace Test {
    internal class Program {
        private static readonly Uri Url = new Uri("https://github.com/Bomret/NeverNull/blob/master/README.md");

        private static void Main(string[] args) {
            Option.Create(() => WebRequest.Create(Url) as HttpWebRequest)
                  .Map(request => request.GetResponse())
                  .Map(response => response.ContentType)
                  .Filter(contentType => contentType.StartsWith("text"))
                  .Match(
                      contentType => Console.WriteLine("Success: {0}", contentType),
                      () => Console.WriteLine("No matching result."));

            Console.ReadKey();
        }
    }
}