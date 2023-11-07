using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public class ContentNegotiationMiddleware
{
    private readonly RequestDelegate _next;

    public ContentNegotiationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string acceptHeader = context.Request.Headers["Accept"];
        string responseContent = null;

        if (acceptHeader.Contains("application/json"))
        {
            responseContent = GenerateJsonResponse();
            context.Response.ContentType = "application/json";
        }
        else if (acceptHeader.Contains("text/html"))
        {
            responseContent = GenerateHtmlTextResponse();
            context.Response.ContentType = "text/html";
        }
        else if (acceptHeader.Contains("text/plain"))
        {
            responseContent = GeneratePlainTextResponse();
            context.Response.ContentType = "text/plain";
        }
        else
        {
            responseContent = GenerateJsonResponse();
            context.Response.ContentType = "application/json";
        }

        var isAjaxRequest = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        if (isAjaxRequest)
        {
            context.Response.ContentType = "text/html";
            responseContent = GeneratePartialView(responseContent);
        }

        await context.Response.WriteAsync(responseContent);
    }

    private string GenerateJsonResponse()
    {
        return "{ \"message\": \"This is a JSON response\" }";
    }

    private string GenerateHtmlTextResponse()
    {
        return "This is an HTML response";
    }

    private string GeneratePlainTextResponse()
    {
        return "This is plain response";
    }

    private string GeneratePartialView(string content)
    {
        return $"<div id=\"partial-view-container\">{content}</div>";
    }
}

