using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses
{
    public class UpdateAppUserCommandRequest : IRequest<Results>
    {
    
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }

        //public Guid  AppUserId{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        

        //public List<Guid> ExpenseCategoryIds { get; set; }

        
    }
}
