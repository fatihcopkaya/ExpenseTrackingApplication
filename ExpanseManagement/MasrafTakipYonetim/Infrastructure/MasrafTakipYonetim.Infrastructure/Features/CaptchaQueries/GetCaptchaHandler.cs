using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.Captcha;
using MasrafTakipYonetim.Application.Dtos.Captcha;
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
    public class GetCaptchaHandler : IRequestHandler<GetCaptchaRequest, Results>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public GetCaptchaHandler(IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Results> Handle(GetCaptchaRequest request, CancellationToken cancellationToken)
        {
            string captcha = Cryptography.CreateCaptchaValue();
            var result = new GetCaptchaDto()
            {
                CaptchaImage = Cryptography.GetCaptchaImageBase64(captcha),
                CaptchaValue = Cryptography.DateEncript(captcha)
            };

            return Results<GetCaptchaDto>.Success(result);
        }
    }
}
