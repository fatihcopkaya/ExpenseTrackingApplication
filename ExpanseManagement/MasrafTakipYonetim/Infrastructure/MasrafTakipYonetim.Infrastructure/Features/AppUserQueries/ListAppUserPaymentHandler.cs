using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Queries.AppUserQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Application.Dtos.ExpenseCategory;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.AppUserQueries
{
    public class ListAppUserPaymentHandler : IRequestHandler<ListAppUserPaymentRequest, Results>
    {
        private readonly IAppUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ListAppUserPaymentHandler(IAppUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Results> Handle(ListAppUserPaymentRequest request, CancellationToken cancellationToken)
        {
            var userlist = await _userRepository.GetListAsync(x => !x.IsDeleted);

            var userListDto = userlist.Select(x => new ListAppUserDto()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
            }).AsQueryable();
            if (userlist.Count == 0)
            {
                throw new MasrafTakipCustomException($"{nameof(AppUser)} Listesi bulunamadı", 404);
            };

            return Results<List<ListAppUserDto>>.Success(userListDto.ToList());
        }
    }
}
