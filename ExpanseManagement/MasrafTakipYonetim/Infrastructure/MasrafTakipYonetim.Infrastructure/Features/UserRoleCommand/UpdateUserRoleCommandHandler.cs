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
    public class UpdateUserRoleCommandHandler : IRequestHandler<UpdateUserRoleCommandRequest, Results>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UpdateUserRoleCommandHandler(IUserRoleRepository userRoleRepository, IMapper mapper, IMediator mediator)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<Results> Handle(UpdateUserRoleCommandRequest request, CancellationToken cancellationToken)
        {
            var existingUserRole = await _userRoleRepository.GetUserRoleById(request.Id);

            if (existingUserRole != null)
            {
                existingUserRole.Data.Id = request.Id;
                existingUserRole.Data.RoleId = request.RoleId;
                existingUserRole.Data.AppUserId = request.AppUserId;
               


                try
                {
                    await _userRoleRepository.UpdateUserRole(existingUserRole.Data);
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
