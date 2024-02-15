using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static readonly HttpClient client = new();
    private static readonly string[] urls =
    {
        "http://localhost:8888/MyNameByHeader",
        "http://localhost:8888/MyName",
        "http://localhost:8888/Information",
        "http://localhost:8888/Success",
        "http://localhost:8888/Redirection",
        "http://localhost:8888/ClientError",
        "http://localhost:8888/ServerError"
    };

    static async Task Main(string[] args)
    {
        await FetchAndPrintResponsesFromUrls();

        Console.ReadLine();
    }

    private static async Task FetchAndPrintResponsesFromUrls()
    {
        foreach (string url in urls)
        {
            await FetchAndPrintResponseFromUrl(url);
        }
    }

    private static async Task FetchAndPrintResponseFromUrl(string url)
    {
        HttpResponseMessage response = await client.GetAsync(url);
        Console.WriteLine($"For url: {url}, received status code: {response.StatusCode}");
        await MaybePrintResponseContent(url, response);

        if (response.Headers.TryGetValues("X-MyName", out var values))
        {
            Console.WriteLine($"For url: {url}, received X-MyName header: {string.Join(",", values)}");
        }
    }

    private static async Task MaybePrintResponseContent(string url, HttpResponseMessage response)
    {
        if (url.EndsWith("MyName", StringComparison.OrdinalIgnoreCase) && response.IsSuccessStatusCode)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Received name: " + responseBody);
        }
    }
}