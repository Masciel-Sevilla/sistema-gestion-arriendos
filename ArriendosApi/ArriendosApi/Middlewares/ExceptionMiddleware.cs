using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
namespace ArriendosApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }
        public async Task InvokeAsync(HttpContext context)
        {

            try {
                await _next(context);
            }
            catch(Exception e) {
                _logger.LogError(e, "Ocurrio un problema no controlado:{Message}",e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception) { 
        
            context.Response.ContentType= "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var problemDetails = new ProblemDetails
            {
                Status = context.Response.StatusCode,
                Title = "Error interno del servidor",
                Detail = _env.IsDevelopment() ? exception.Message : "Ocurrio un error",
                Instance = context.Request.Path
            };

            var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json=JsonSerializer.Serialize(problemDetails, jsonOptions);
            return context.Response.WriteAsync(json);
        }
    }
}
