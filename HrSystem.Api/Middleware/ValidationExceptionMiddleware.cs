using System.ComponentModel.DataAnnotations;
using System.Net;
using FluentValidation;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HrSystem.Api.Middleware
{
    public class ValidationExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (FluentValidation.ValidationException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";

                var errors = ex.Errors
                   .GroupBy(e => e.PropertyName)
                   .ToDictionary(
                       g => g.Key,
                       g => g.Select(e => e.ErrorMessage).Distinct().ToArray()
                   );

                var payload = new
                {
                    status = 400,
                    title = "Validation Failed",
                    errors
                };
                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(payload));



                if (ex.Message.Contains("Invalid email or password"))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized; // أو 400
                                                                                    // منع إظهار مسار الخطأ
                    await context.Response.WriteAsync("{\"error\": \"Invalid email or password.\"}");
                    return;
                }


            }
        }
    }

    public static class ValidationExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidationExceptionMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ValidationExceptionMiddleware>();
        }
    }
}
