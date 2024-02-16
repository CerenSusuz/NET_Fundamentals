using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace APITests
{
    [TestClass]
    public class ProductTests
    {
        private static readonly HttpClient client = new();

        [TestMethod]
        public async Task Test_Get_Products()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7220/api/Products?pageNumber=1&pageSize=10");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(responseBody);
        }

        [TestMethod]
        public async Task Test_Get_Product()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:7220/api/Products/1");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(responseBody);
        }

        [TestMethod]
        public async Task Test_Post_Product()
        {
            var newProduct = new { Name = "Test Product", Price = 100 };
            var content = new StringContent(JsonConvert.SerializeObject(newProduct), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("https://localhost:7220/api/Products", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(responseBody);
        }

        [TestMethod]
        public async Task Test_Put_Product()
        {
            var updatedProduct = new { Name = "Updated Test Product", Price = 200 };
            var content = new StringContent(JsonConvert.SerializeObject(updatedProduct), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync("https://localhost:7220/api/Products/1", content);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(responseBody);
        }

        [TestMethod]
        public async Task Test_Delete_Product()
        {
            HttpResponseMessage response = await client.DeleteAsync("https://localhost:7220/api/Products/1");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            Assert.IsNotNull(responseBody);
        }
    }
}