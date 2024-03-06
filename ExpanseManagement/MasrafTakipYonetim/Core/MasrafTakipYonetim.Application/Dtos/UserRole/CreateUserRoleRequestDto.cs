using System;


namespace MasrafTakipYonetim.Application.Dtos.RoleUser
{
    public class CreateUserRoleRequestDto
    {
        public Guid RoleId { get; set; }
        
        public Guid AppUserId { get; set; }
       
    }
}
