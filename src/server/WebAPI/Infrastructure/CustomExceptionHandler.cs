using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;

namespace WebAPI.Infrastructure;

public class CustomExceptionHandler : IExceptionHandler
{
    private readonly Dictionary<Type, Func<HttpContext, Exception, Task>> _exceptionHandlers;
    public CustomExceptionHandler()
    {
        _exceptionHandlers = new Dictionary<Type, Func<HttpContext, Exception, Task>>
        {
            // { typeof(ValidationException), HandleValidationException }
        };
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionType = exception.GetType();
        if (_exceptionHandlers.ContainsKey(exceptionType))
        {
            await _exceptionHandlers[exceptionType].Invoke(httpContext, exception);
            return true;
        }
        else
            await HandleUnknowException(httpContext, exception);

        return false;
    }

    private async Task HandleUnknowException(HttpContext httpContext, Exception ex)
    {
        string message;
        if (ex.InnerException != null && !String.IsNullOrEmpty(ex.InnerException.Message))
            message = ex.InnerException.Message;
        else
            message = ex.Message;

        string? messageLine = null;
        StackTrace? st = new StackTrace(ex, true);
        var frame = st.GetFrame(0);
        if (frame != null)
        {
            var file = frame.GetFileName();
            var line = frame.GetFileLineNumber();

            messageLine = $"(file: {file}, line: {line})";
        }
        if (messageLine != null)
            message += messageLine;

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(message);
    }
}
