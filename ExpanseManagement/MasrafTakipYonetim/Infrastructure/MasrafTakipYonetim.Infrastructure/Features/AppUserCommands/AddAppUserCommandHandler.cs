using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Helpers;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Infrastructure.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.AppUserCommands
{
    public class AddAppUserCommandHandler : IRequestHandler<CreateAppUserCommandRequest, Results>
    {
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;
        private readonly IMailHelper _mailHelper;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IUserExpenseCategoryRepository _userExpenseCategoryRepository;
        //private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        

        public AddAppUserCommandHandler(IAppUserService appUserService, IMapper mapper, IAppUserRepository appUserRepository, IMailHelper mailHelper,IUserExpenseCategoryRepository userExpenseCategoryRepository)
        {
            _appUserService = appUserService;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _mailHelper = mailHelper;
            _userExpenseCategoryRepository = userExpenseCategoryRepository;


        }


        public async Task<Results> Handle(CreateAppUserCommandRequest request, CancellationToken cancellationToken)
        {
            var newAppUser = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber,
                RoleId= request.RoleId,

            };

            var createdAppUser = await _appUserService.CreateAppUser(newAppUser);

            var expenseCategory = new UserExpenseCategory
            {
                ExpenseCategoryId = request.ExpenseCategoryId,
                AppUserId = createdAppUser.Data.Id,

            };
            var userExpenseCategory= await _userExpenseCategoryRepository.CreateUserExpenseCategoryUser(expenseCategory);
            if (createdAppUser == null)
            {
                
                return Results.Failure(new List<string> { $"{nameof(AppUser)} Oluşturulamadı" });
            }


            if (createdAppUser?.Data != null) // Eğer veri varsa
            {
                var appUser = createdAppUser.Data; // IDataResult içindeki AppUser verisine erişim
                 await _mailHelper.SendMail(
                    toMail: appUser.Email, // Kullanıcının e-posta adresi
                    subject: "Yeni Kullanıcı Oluşturuldu",
                    mailBody: $"Merhaba {appUser.FirstName},\n\nKullanıcı hesabınız başarıyla oluşturuldu. Şifreniz: {request.Password}\n\nİyi günler dileriz!"
                   // fromMail: "masraftakipyonetim23@gmail.com",
                   //password: "zzwgihkpnifbxcuw"
                );
            }



            


            return Results.Success();

        }
    
    }
}
