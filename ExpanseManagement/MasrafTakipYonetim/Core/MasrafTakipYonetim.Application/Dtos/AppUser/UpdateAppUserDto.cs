
using MasrafTakipYonetim.Domain.Enums;

namespace MasrafTakipYonetim.Application.Dtos.AppUser
{
    public class UpdateAppUserDto
    {
        public Guid AppUserId { get; set; }
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
        public Guid ExpenseCategoryId { get; set; }

        //public ExpenseCategoryType? ExpenseCategoryType { get; set; }

    }
}
