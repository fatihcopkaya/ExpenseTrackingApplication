using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.ChangePasswordRequestAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, Results>
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appuserservice;

        public ChangePasswordHandler(IAppUserRepository userRepository, IMapper mapper, IAppUserService appUserService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _appuserservice = appUserService;
        }

        public async Task<Results> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {


            var user = await _appuserservice.GetAuthenticatedUser();
            //user.Data.Id = request.AppUserId;


            if (user.Data == null)
            {
                throw new MasrafTakipCustomException($"{nameof(AppUser)} Kullanıcısı bulunamadı", 404);
            }



            var verifyOldPassword = await _appuserservice.VerifyPasswordHash(request.OldPassword, user.Data.PasswordHash, user.Data.PasswordSalt);
            
           
                if (verifyOldPassword==false)
                {
                return Results.Failure(new List<string> { "Eski şifre hatalı." });

            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                // Yeni şifreler eşleşmiyorsa hata döndür
                return Results.Failure((new List<string> { "Yeni şifreler eşleşmiyor." }));
            }

            var result =_appuserservice.CreatePasswordHash(request.NewPassword);
            user.Data.PasswordHash = result.PasswordHash;
            user.Data.PasswordSalt = result.PasswordSalt;

            //appuser.Password = request.NewPassword;


            // UserRepository aracılığıyla güncelleme yapılıyor
            await _userRepository.UpdateAppUser(user.Data);


            return Results<ChangePasswordResponse>.Success(new ChangePasswordResponse
            {
                isSuccess = true,
                Message = "Password changed successfully.",
               
            });
            // return Results.Success();
        }
    }
}
