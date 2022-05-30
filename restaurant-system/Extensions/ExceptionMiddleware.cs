using restaurant_system.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.ComponentModel.DataAnnotations; // ValidationException
using System.Threading.Tasks;
using System;

namespace restaurant_system.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            await _next(httpContext);
            try
            {
             
            }
            catch (ValidationException ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await HandleExceptionAsync(httpContext, new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                await HandleExceptionAsync(httpContext, new ErrorDetails()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    Message = ErrorMesseges.Common
                    //Message = ex.Message

                });
                
            }

        }
        private async Task HandleExceptionAsync(HttpContext context, ErrorDetails errorDetails)
        {
            await context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
