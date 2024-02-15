using System;
using System.Net;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        HttpListener server = new();
        server.Prefixes.Add("http://localhost:8888/");
        server.Start();

        Console.WriteLine("Server started...");

        while (true)
        {
            HttpListenerContext context = await server.GetContextAsync();
            HttpListenerRequest request = context.Request;

            string methodName = request.RawUrl.Trim('/');
            switch (methodName)
            {
                case "MyName":
                    await GetMyName(context.Response);
                    break;

                default:
                    context.Response.StatusCode = 404;
                    break;
            }
        }
    }

    static async Task GetMyName(HttpListenerResponse response)
    {
        string name = "Gerund";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(name);
        response.ContentLength64 = buffer.Length;
        await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        response.Close();
    }
}