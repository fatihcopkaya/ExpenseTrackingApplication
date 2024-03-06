using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.UserRoleRequestsAndResponses;
using MasrafTakipYonetim.Application.Cqrs.Queries.UserRoleQuery;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.RoleUser;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.UserRoleCommand
{
    public class DeleteUserRoleCommandHandler : IRequestHandler<DeleteUserRoleCommandRequest, Results>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public DeleteUserRoleCommandHandler(IUserRoleRepository userRoleRepository, IMapper mapper,IMediator mediator)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<Results> Handle(DeleteUserRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var roleUserToDelete = await _userRoleRepository.GetUserRoleById(request.Id);

           
               


                if (roleUserToDelete != null)
                {
                    roleUserToDelete.Data.IsDeleted = true;
                    var deletedUserRole = await _userRoleRepository.DeleteUserRole(roleUserToDelete.Data);

                    if (deletedUserRole.Success)
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
