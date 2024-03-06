using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.RoleRequestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Infrastructure.Features.RoleCommands
{
    public class DeleteRolesCommandHandler : IRequestHandler<DeleteRoleCommandRequest, Results>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;

        public DeleteRolesCommandHandler(IMediator mediator, IMapper mapper, IRoleRepository roleRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        public async Task<Results> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var roleToDelete = await _roleRepository.GetRoleById(request.Id);

            if (roleToDelete != null)
            {
                roleToDelete.Data.IsDeleted = true;
                var deletedRole = await _roleRepository.DeleteRoleAsync(roleToDelete.Data);

                if (deletedRole.Success)
                {
                    return Results.Success();
                }
                else
                {
                    throw new MasrafTakipCustomException($"{request.Id} ID'sine sahip rol silinemedi.", 500);
                }
            }
            else
            {
                throw new MasrafTakipCustomException($"{request.Id} ID'sine sahip rol bulunamadı.", 404);
            }
        }
    }
}
