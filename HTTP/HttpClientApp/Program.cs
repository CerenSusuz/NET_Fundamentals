using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static readonly HttpClient client = new();

    static async Task Main(string[] args)
    {
        HttpResponseMessage response = await client.GetAsync("http://localhost:8888/MyName");

        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Received response: " + responseBody);

        Console.ReadLine();
    }
}