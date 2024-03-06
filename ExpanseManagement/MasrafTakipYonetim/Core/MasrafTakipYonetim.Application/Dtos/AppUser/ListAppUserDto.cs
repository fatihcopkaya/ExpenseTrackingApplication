using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Dtos.AppUser
{
    public class ListAppUserDto
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid ExpenseCategoryId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ExpenseCategoryName { get; set; }
        public string ?RoleName { get; set; }
       

    }
}
