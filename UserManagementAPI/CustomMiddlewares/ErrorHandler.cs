using System.Net;
using System.Text.Json;

namespace UserManagementAPI.CustomMiddlewares;

public class ErrorHandler
{
    private readonly RequestDelegate _next;

    public ErrorHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var result = JsonSerializer.Serialize(new { message = "Internal server error" });
        response.StatusCode = (int)HttpStatusCode.InternalServerError;

        return response.WriteAsync(result);
    }
}