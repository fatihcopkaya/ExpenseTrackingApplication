using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.Captcha;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results = MasrafTakipYonetim.Application.Commons.Results;

namespace MasrafTakipYonetim.Infrastructure.Features.CaptchaQueries
{
    public class VerifyCaptchaHandler : IRequestHandler<VerifyCaptchaRequest, Results>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public VerifyCaptchaHandler(IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public Task<Results> Handle(VerifyCaptchaRequest request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.CaptchaCode) || string.IsNullOrWhiteSpace(request.CaptchaValue))
            {
                return Task.FromResult(Results.Failure(new List<string>() { "Code değeri yanlış girildi." }));

            }

            if (!Cryptography.ValidateDate(request.CaptchaValue, request.CaptchaCode))
            {
                return Task.FromResult(Results.Failure(new List<string>() { "Code değeri yanlış girildi." }));
            }
            return Task.FromResult(Results.Success());

        }
    }
}
