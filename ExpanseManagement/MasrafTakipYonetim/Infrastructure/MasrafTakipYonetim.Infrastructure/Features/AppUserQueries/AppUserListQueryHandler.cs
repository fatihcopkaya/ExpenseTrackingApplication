
using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Application.Utilities.Result;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using MasrafTakipYonetim.Domain.Enums;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Linq.Expressions;
using StackExchange.Redis;

namespace MasrafTakipYonetim.Infrastructure.Features.AppUserQueries
{
    public class AppUserListQueryHandler : IRequestHandler<AppUserListQueryRequest, Results>
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserExpenseCategoryRepository _userExpenseCategoryRepository;

        public AppUserListQueryHandler(IAppUserRepository userRepository, IMapper mapper, IUserExpenseCategoryRepository userExpenseCategoryRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userExpenseCategoryRepository = userExpenseCategoryRepository;
        }
        public async Task<Results> Handle(AppUserListQueryRequest request, CancellationToken cancellationToken)
        {


            //var userlist = await _userExpenseCategoryRepository.GetListAsync(p => p.IsDeleted == false && p.AppUser.IsDeleted == false && p.ExpenseCategory.IsDeleted == false 
            //&& p.ExpenseCategory.ExpenseCategoryType == request.ExpenseCategoryType,
            //includes: new Expression<Func<Role, object>>[]
            //{
            //    x => x.AppUser,
            //    x=> x.Role
            //}); 
            var userlist = await _userRepository.GetAppUserListWithRoles();



            var userListDto = userlist.Data.Select(x => new ListAppUserDto()
            {   Id= x.Id,
                RoleId= x.RoleId,
               
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                RoleName=x.Role.Name,
                

            }).AsQueryable();
            if (userlist.Data.Count == 0)
            {
                throw new MasrafTakipCustomException($"{nameof(AppUser)} Listesi bulunamadı", 404);
            };
            
            Results result;

            if (!string.IsNullOrEmpty(request.FirstName) || !string.IsNullOrEmpty(request.LastName) || !string.IsNullOrEmpty(request.Email) || !string.IsNullOrEmpty(request.PhoneNumber))
            {
                userListDto = userListDto.Where(x =>
                    (string.IsNullOrEmpty(request.FirstName) ||
                     x.FirstName.ToLower().Contains(request.FirstName.Trim().ToLower()) ||
                     x.FirstName.ToUpper().Contains(request.FirstName.Trim().ToUpper())) &&
                    (string.IsNullOrEmpty(request.LastName) ||
                     x.LastName.ToLower().Contains(request.LastName.Trim().ToLower()) ||
                     x.LastName.ToUpper().Contains(request.LastName.Trim().ToUpper())) &&
                       (string.IsNullOrEmpty(request.Email) ||
                     x.Email.ToLower().Contains(request.Email.Trim().ToLower()) ||
                     x.Email.ToUpper().Contains(request.Email.Trim().ToUpper())) &&
                      (string.IsNullOrEmpty(request.PhoneNumber) ||
                     x.PhoneNumber.ToLower().Contains(request.PhoneNumber.Trim().ToLower()) ||
                     x.PhoneNumber.ToUpper().Contains(request.PhoneNumber.Trim().ToUpper())) 
                    


                );
            }



            if (!string.IsNullOrEmpty(request.SortField))
            {
                //userlist = userlist.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
                userListDto = userListDto.CustomOrderBy(request.SortField, request.SortOrder == 1 ? true : false);
            }
           


            else
            {
                result = await Task.FromResult(userListDto.OrderBy(x => x.FirstName)
                .QueryResourceNotMapped<ListAppUserDto>(request.PageNumber, request.PageSize));
            }

            result = await Task.FromResult(userListDto.QueryResourceNotMapped<ListAppUserDto>(request.PageNumber, request.PageSize));
            return result;
        }
    }
}

//if (!string.IsNullOrEmpty(request.FilterField) && !string.IsNullOrEmpty(request.FilterValue))
//{
//    // Örnek: İsim alanına göre filtreleme
//    if (request.FilterField.Equals("FirstName"))
//    {
//        userListDto = userListDto.Where(userlist => userlist.FirstName.Contains(request.FilterValue));
//    }
//    // Diğer filtreleme türlerini eklemeye devam edebilirsiniz.
//}