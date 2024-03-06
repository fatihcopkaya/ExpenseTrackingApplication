//using AutoMapper;
//using MasrafTakipYonetim.Application.Commons;
//using MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses;
//using MasrafTakipYonetim.Application.CustomExceptions;
//using MasrafTakipYonetim.Application.Dtos.AppUser;
//using MasrafTakipYonetim.Application.Helpers;
//using MasrafTakipYonetim.Application.Repositories;
//using MasrafTakipYonetim.Application.Services;
//using MasrafTakipYonetim.Domain.Entities;
//using MasrafTakipYonetim.Domain.Enums;
//using MasrafTakipYonetim.Persistence.Contacts;
//using MassTransit.Internals;
//using MediatR;
//using Microsoft.Extensions.Logging;

//namespace MasrafTakipYonetim.Infrastructure.Features.AppUserCommands
//{
//    public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommandRequest, Results>
//    {
//        private readonly IAppUserService _appUserService;
//        private readonly IMapper _mapper;
//        private readonly IMailHelper _mailHelper;
//        private readonly IAppUserRepository _appUserRepository;
//        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
//        private readonly ILogger<CreateAppUserCommandHandler> _logger;

//        public CreateAppUserCommandHandler(IAppUserService appUserService, IMapper mapper, IAppUserRepository appUserRepository, IExpenseCategoryRepository expenseCategoryRepository, IMailHelper mailHelper, ILogger<CreateAppUserCommandHandler> logger)
//        {
//            _appUserService = appUserService;
//            _mapper = mapper;
//            _mailHelper = mailHelper;
            

//            _appUserRepository = appUserRepository; 
//            _expenseCategoryRepository = expenseCategoryRepository;
//            _logger = logger;
//        }

    

//        public async Task<Results> Handle(CreateAppUserCommandRequest request, CancellationToken cancellationToken)
//        {



//                var newUserExpenseCategorys = new List<UserExpenseCategory>();

//            foreach (var item in request.ExpenseCategoryIds)
//            {
//                newUserExpenseCategorys.Add(new UserExpenseCategory()
//                {
//                    ExpenseCategoryId = item,

//                });
//            }

 


//                var newAppUser = new AppUser
//                {
//                    FirstName = request.FirstName,
//                    LastName = request.LastName,
//                    Email = request.Email,
//                    Password = request.Password,
//                    PhoneNumber = request.PhoneNumber,
//                    UserExpenseCategory = newUserExpenseCategorys,
                   








//                    //UserExpenseCategory=new UserExpenseCategory() { ExpenseCategoryId=request.}


//                    // Diğer özellikleri burada da atanabilir.
//                };



//                var createdAppUser = await _appUserService.CreateAppUser(newAppUser);

//                if (createdAppUser == null)
//                {
//                    _logger.LogInformation("Oluşturalamadı");
//                    return Results.Failure(new List<string> { $"{nameof(AppUser)} Oluşturulamadı" });
//                }

//            if (createdAppUser?.Data != null) // Eğer veri varsa
//            {
//                var appUser = createdAppUser.Data; // IDataResult içindeki AppUser verisine erişim
//                _mailHelper.SendMail(
//                    toMail: appUser.Email, // Kullanıcının e-posta adresi
//                    subject: "Yeni Kullanıcı Oluşturuldu",
//                    mailBody: $"Merhaba {appUser.FirstName},\n\nKullanıcı hesabınız başarıyla oluşturuldu. Şifreniz: {request.Password}\n\nİyi günler dileriz!",
//                    fromMail: "masraftakipyonetim23@gmail.com",
//                    password: "zzwgihkpnifbxcuw"
//                );
//            }
//            _logger.LogInformation("Success");
//            return Results.Success();
           
//            }

//        }
//    }
         
