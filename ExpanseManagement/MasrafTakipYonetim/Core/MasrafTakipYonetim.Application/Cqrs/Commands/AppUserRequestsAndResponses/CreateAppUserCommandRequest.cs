using MasrafTakipYonetim.Application.Commons;
using MasrafTakipYonetim.Application.Dtos.AppUser;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Domain.Enums;
using MediatR;


namespace MasrafTakipYonetim.Application.Cqrs.Commands.AppUserRequestsAndResponses
{
    public class CreateAppUserCommandRequest : IRequest<Results>
    {
        // public CreateAppUserDto createAppUserDto { get; set; }

        //public Guid ExpenseCategoryId { get; set; }
       
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public Guid ExpenseCategoryId { get; set; }


        //public List<string> ExpenseCategoryName { get; set; }

        //public string firstname = "";
        //public List<Guid> ExpenseCategoryIds { get; set; }

        //public ExpenseCategory ExpenseCategory { get; set; }
    }
}
