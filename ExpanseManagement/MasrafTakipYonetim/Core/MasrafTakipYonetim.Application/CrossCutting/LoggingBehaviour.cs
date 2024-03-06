using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.CrossCutting
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
                                                                                                where TResponse : Results
    {
        private readonly ILogger<TRequest> _logger;
        

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
            
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            this._logger.LogInformation($"Request: {JsonConvert.SerializeObject(request)}");

            var response = await next(); // next'i sadece bir kez çağır ve yanıtı değişkende sakla

            this._logger.LogInformation($"Response: {JsonConvert.SerializeObject(response)}");

            return response;
        }





    }
}
