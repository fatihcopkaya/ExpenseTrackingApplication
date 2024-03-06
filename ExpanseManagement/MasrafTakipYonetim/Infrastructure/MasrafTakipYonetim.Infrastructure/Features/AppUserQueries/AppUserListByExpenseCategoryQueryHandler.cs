 using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Dtos.Payment;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Services;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MasrafTakipYonetim.Infrastructure.Features.AppUserQueries
{
    public class AppUserListByExpenseCategoryQueryHandler : IRequestHandler<AppUserListByExpenseCategoryRequest, Results>
    {

        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserExpenseCategoryRepository _userExpenseCategoryRepository;
        private readonly IExpenseCategoryRepository _expenseCategoryRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        //private readonly IRedisService _redisService;

        public AppUserListByExpenseCategoryQueryHandler(IAppUserRepository userRepository, IMapper mapper, 
            IUserExpenseCategoryRepository userExpenseCategoryRepository, IExpenseCategoryRepository 
            expenseCategoryRepository, IUserRoleRepository userRoleRepository
            /*, IRedisService redisService*/)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userExpenseCategoryRepository = userExpenseCategoryRepository;
            _expenseCategoryRepository = expenseCategoryRepository;
            _userRoleRepository = userRoleRepository;
           // _redisService = redisService;
        }

        public async Task<Results> Handle(AppUserListByExpenseCategoryRequest request, CancellationToken cancellationToken)
        {
            var appuserlist = await _userExpenseCategoryRepository.GetAppUserlistByExpenseCategoryTypeAsync(request.ExpenseCategoryType);

            var appUserByExpensecategoryDto = appuserlist.Data.Select( A => new ListAppUserByExpenseCategoryDto()
            {
                Id = A.Id,
                AppUserId = A.AppUserId,
                
                FirstName = A.AppUser.FirstName,
                LastName = A.AppUser.LastName,
                PhoneNumber = A.AppUser.PhoneNumber,
                Email = A.AppUser.Email,
                ExpenseCategoryId = A.ExpenseCategoryId,
                ExpenseCategoryName = A.ExpenseCategory?.Name,
               
            }).AsQueryable();

            if (appuserlist.Data.Count == 0)
            {
                throw new MasrafTakipCustomException($"{nameof(UserExpenseCategory)} Listesi bulunamadı", 404);
            };

            Results result;

            if (!string.IsNullOrEmpty(request.FirstName) || !string.IsNullOrEmpty(request.LastName) || !string.IsNullOrEmpty(request.Email) || !string.IsNullOrEmpty(request.PhoneNumber))
            {
                appUserByExpensecategoryDto = appUserByExpensecategoryDto.Where(x =>
                    (string.IsNullOrEmpty(request.FirstName) ||
                     x.FirstName.ToLower().Contains(request.FirstName.Trim().ToLower()) ||
                     x.FirstName.ToUpper().Contains(request.FirstName.Trim().ToUpper())) &&
                    (string.IsNullOrEmpty(request.LastName) ||
                     x.LastName.ToLower().Contains(request.LastName.Trim().ToLower()) ||
                     x.LastName.ToUpper().Contains(request.LastName.Trim().ToUpper()))&&
                       (string.IsNullOrEmpty(request.Email) ||
                     x.Email.ToLower().Contains(request.Email.Trim().ToLower()) ||
                     x.Email.ToUpper().Contains(request.Email.Trim().ToUpper()))&&
                      (string.IsNullOrEmpty(request.PhoneNumber) ||
                     x.PhoneNumber.ToLower().Contains(request.PhoneNumber.Trim().ToLower()) ||
                     x.PhoneNumber.ToUpper().Contains(request.PhoneNumber.Trim().ToUpper())) 


                );
            }


            if (!string.IsNullOrEmpty(request.SortField))
            {
                //userlist = userlist.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
                appUserByExpensecategoryDto = appUserByExpensecategoryDto.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
            }
            else
            {
                result = await Task.FromResult(appUserByExpensecategoryDto.OrderBy(x => x.AppUserId)
                    .QueryResourceNotMapped<ListAppUserByExpenseCategoryDto>(request.PageNumber, request.PageSize));
            }



            result = await Task.FromResult(appUserByExpensecategoryDto.QueryResourceNotMapped<ListAppUserByExpenseCategoryDto>(request.PageNumber, request.PageSize));
            return result;


        }
    }
}
