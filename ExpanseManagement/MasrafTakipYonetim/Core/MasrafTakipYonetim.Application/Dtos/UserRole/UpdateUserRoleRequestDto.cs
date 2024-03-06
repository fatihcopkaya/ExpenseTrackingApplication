
namespace MasrafTakipYonetim.Application.Dtos.RoleUser
{
    public class UpdateUserRoleRequestDto
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }

        public Guid AppUserId { get; set; }
    }
}
