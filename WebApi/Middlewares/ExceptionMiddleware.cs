using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.Errors;

namespace WebApi.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next,
                                    ILogger<ExceptionMiddleware> logger,
                                    IHostEnvironment env)
        {
            this.env = env;
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                ApiError response;
                HttpStatusCode staCode= HttpStatusCode.InternalServerError;
                String message;
                var exceptionType=ex.GetType();

                if(exceptionType==typeof(UnauthorizedAccessException)){
                    staCode=HttpStatusCode.Forbidden;
                    message="You are not authorized";
                }else
                {
                    staCode=HttpStatusCode.InternalServerError;
                    message="Some Unkhown Error Occured";
                }

                if(env.IsDevelopment()){
                    response=new ApiError((int)staCode,ex.Message,ex.StackTrace.ToString());
                }else{
                    response=new ApiError((int)staCode,message);
                }

                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)staCode;
                context.Response.ContentType="application/json";
                await context.Response.WriteAsync(response.ToString());
            }
        }
    }
}