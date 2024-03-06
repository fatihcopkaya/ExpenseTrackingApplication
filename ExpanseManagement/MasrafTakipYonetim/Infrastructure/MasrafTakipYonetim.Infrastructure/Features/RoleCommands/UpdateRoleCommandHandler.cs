using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.RoleRequestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.Roles;
using MasrafTakipYonetim.Application.QueriesRequestsAndResponses.RoleQuery;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Infrastructure.Features.RoleQueries;
using MasrafTakipYonetim.Persistence.Contacts;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.RoleCommands
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommandRequest, Results>
    {
        private readonly IRoleRepository _roleRepository;

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IMediator mediator, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Results> Handle(UpdateRoleCommandRequest request, CancellationToken cancellationToken)
        {
           
            var existingRole = await _roleRepository.GetRoleById(request.Id);

            if (existingRole != null)
            {
                existingRole.Data.Name = request.Name;

                try
                {
                    await _roleRepository.UpdateRoleAsync(existingRole.Data);
                    return Results.Success();
                }
                catch (Exception ex)
                {
                    throw new MasrafTakipCustomException($"{request.Id} ID'sine sahip rol güncellenirken bir hata oluştu: {ex.Message}", 500);
                }
            }
            else
            {
                throw new MasrafTakipCustomException($"{request.Id} ID'sine sahip rol bulunamadı.", 404);
            }
        }
    }
}
