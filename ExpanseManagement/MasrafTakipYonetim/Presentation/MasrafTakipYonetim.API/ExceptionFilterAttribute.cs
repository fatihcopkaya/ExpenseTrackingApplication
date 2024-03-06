using MasrafTakipYonetim.Application.CustomExceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.Extensions.Logging;

namespace MasrafTakipYonetim.API
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> _logger;
        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }
        public async override Task OnExceptionAsync(ExceptionContext context)
        {
            //Fırlatılan exception'ın status code'unu bilemediğimiz durumda 
            //default olarak '500 Internal Server Error'ı belirliyoruz.
            var statusCode = HttpStatusCode.InternalServerError;

            //Fırlatılan exception DataNotFoundException ise
            //status code'u '404 Not Found' yapıyoruz.
            if (context.Exception is MasrafTakipCustomException customException)
            {
                
                statusCode = (HttpStatusCode)customException.StatusCode;

            }
            else
            {
                //customexception ile yakalayamadığımız exceptionlar buraya gelip loglanacak
                _logger.LogError(context.Exception, "Ele alınmayan bir istisna oluştu");
            }
               
           

            //Bu request'e karşılık verilecek response'a status code'u ve
            //result'u değiştirerek dönebiliriz.
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)statusCode;

            context.Result = new JsonResult(new
            {
                error = new[] { context.Exception.Message },
                statusCode = (int)statusCode,
                stackTrace = context.Exception.StackTrace
            });
        }
    }
}
