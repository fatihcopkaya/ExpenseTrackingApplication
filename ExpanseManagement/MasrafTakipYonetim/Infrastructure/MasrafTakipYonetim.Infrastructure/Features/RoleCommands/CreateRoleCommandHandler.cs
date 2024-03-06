using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.RoleRequestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.Roles;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Persistence.Contacts;
using MediatR;
using StackExchange.Redis;

namespace MasrafTakipYonetim.Infrastructure.Features.RoleCommands
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommandRequest, Results>
    {

        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<Results> Handle(CreateRoleCommandRequest request, CancellationToken cancellationToken)
        {
           
            var newRole = new Roles
            {
                Name =request.Name,

            };
            
            var createdRole = await _roleRepository.CreateRole(newRole);
            if(!createdRole.Success)
            {
                throw new MasrafTakipCustomException($"{nameof(Roles)} oluşturulamadı",500);

            }
            return Results.Success();
           

           


           


        }
    }
}
