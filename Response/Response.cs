using System.Net;
using Microsoft.AspNetCore.Identity;

namespace AuthRazor.Response;

public class Response<T>
{
    public T? Data { get; set; }
    public List<string> Errors { get; set; } = new();
    public int StatusCode { get; set; }

    public Response(T data)
    {
        Data = data;
        StatusCode = 200;
    }

    public Response(HttpStatusCode code, string message)
    {
        StatusCode = (int)code;
        Errors.Add(message);
    }
    
    public Response(HttpStatusCode code, List<string> message)
    {
        StatusCode = (int)code;
        Errors.AddRange(message);
    }

   
}