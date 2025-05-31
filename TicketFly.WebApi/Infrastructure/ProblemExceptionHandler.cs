using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketFly.Domain.Exceptions;

namespace TicketFly.WebApi.Infrastructure;

public class ProblemExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        if (exception is not ProblemException problemException)
        {
            return true;
        }

        var problemDetails = new ProblemDetails
        {
            Title = problemException.Error,
            Detail = problemException.Message,
            Status = StatusCodes.Status400BadRequest,
            Type = "Bad Request"
        };

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
}
