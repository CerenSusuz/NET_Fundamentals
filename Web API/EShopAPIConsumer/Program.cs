using EShopAPI.Models;
using Newtonsoft.Json;

class Program
{
    private static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        await GetProducts(1, 10, null);
    }

    private static async Task GetProducts(int pageNumber, int pageSize, int? categoryId)
    {
        try
        {
            string url = $"http://localhost:5180/api/products?pageNumber={pageNumber}&pageSize={pageSize}";

            if (categoryId.HasValue)
            {
                url += $"&categoryId={categoryId.Value}";
            }

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<Product>>(responseBody);

            foreach (var product in products)
            {
                Console.WriteLine($"Product Id: {product.ProductID}, Product Name: {product.ProductName}");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", e.Message);
        }
    }
}