using AutoMapper;
using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Cqrs.Commands.RoleUserRequestsAndResponses;
using MasrafTakipYonetim.Application.CustomExceptions;
using MasrafTakipYonetim.Application.Dtos.RoleUser;
using MasrafTakipYonetim.Application.Repositories;
using MasrafTakipYonetim.Domain.Entities;
using MediatR;


namespace MasrafTakipYonetim.Infrastructure.Features.UserRoleCommand
{
    public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommandRequest, Results>
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        public CreateUserRoleCommandHandler(IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }
        public async Task<Results> Handle(CreateUserRoleCommandRequest request, CancellationToken cancellationToken)
        {
           
            var userRole = new UserRole
            {
                RoleId= request.RoleId,
                AppUserId= request.AppUserId,

            };

            var createduserRole = await _userRoleRepository.CreateUserRole(userRole);
            if (!createduserRole.Success)
            {
                throw new MasrafTakipCustomException($"{nameof(UserRole)} Could Not Be Created!", 500);
            }
            return Results.Success();

        }
    }
}
