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
                SetResponseStatus(context.Response, HttpStatusCode.Continue);
                break;
            case "Success":
                SetResponseStatus(context.Response, HttpStatusCode.OK);
                break;
            case "Redirection":
                SetResponseStatus(context.Response, HttpStatusCode.MultipleChoices);
                break;
            case "ClientError":
                SetResponseStatus(context.Response, HttpStatusCode.BadRequest);
                break;
            case "ServerError":
                SetResponseStatus(context.Response, HttpStatusCode.InternalServerError);
                break;
            case "MyNameByHeader":
                var response1 = context.Response;
                response1.Headers.Add("X-MyName", "Ceren");
                SetResponseStatus(response1, HttpStatusCode.OK);
                response1.Close();
                break;
            case "MyNameByCookies":
                SetMyNameByCookies(context.Response);
                break;
            default:
                SetResponseStatus(context.Response, HttpStatusCode.NotFound);
                break;
        }

        context.Response.Close();
    }

    private static void SetResponseStatus(HttpListenerResponse response, HttpStatusCode statusCode)
    {
        response.StatusCode = (int)statusCode;
    }

    private static async Task SendNameInResponse(HttpListenerResponse response)
    {
        string name = "Gerund";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(name);
        response.ContentLength64 = buffer.Length;

        await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
    }

    private static void SetMyNameByCookies(HttpListenerResponse response)
    {
        var cookie = new Cookie("MyName", "Cero from Cookie");
        response.Cookies.Add(cookie);
        SetResponseStatus(response, HttpStatusCode.OK);
    }
}