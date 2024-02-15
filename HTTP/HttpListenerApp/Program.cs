using System;
using System.Net;
using System.Threading.Tasks;

class Program
{
    private const string URL = "http://localhost:8888/";
    private static HttpListener? server;

    static async Task Main(string[] args)
    {
        StartServer();

        while (true)
        {
            await HandleIncomingRequest();
        }
    }

    private static void StartServer()
    {
        server = new HttpListener();
        server.Prefixes.Add(URL);
        server.Start();

        Console.WriteLine("Server started...");
    }

    private static async Task HandleIncomingRequest()
    {
        HttpListenerContext context = await server.GetContextAsync();
        HttpListenerRequest request = context.Request;

        string resourceName = request.RawUrl.Trim('/');

        switch (resourceName)
        {
            case "MyName":
                await SendNameInResponse(context.Response);
                break;
            case "Information":
                SetResponseStatus(context.Response, 100);
                break;
            case "Success":
                SetResponseStatus(context.Response, 200);
                break;
            case "Redirection":
                SetResponseStatus(context.Response, 300);
                break;
            case "ClientError":
                SetResponseStatus(context.Response, 400);
                break;
            case "ServerError":
                SetResponseStatus(context.Response, 500);
                break;
            default:
                SetResponseStatus(context.Response, 404);
                break;
        }

        context.Response.Close();
    }

    private static void SetResponseStatus(HttpListenerResponse response, int statusCode)
    {
        response.StatusCode = statusCode;
    }

    private static async Task SendNameInResponse(HttpListenerResponse response)
    {
        string name = "Gerund";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(name);
        response.ContentLength64 = buffer.Length;

        await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
    }
}