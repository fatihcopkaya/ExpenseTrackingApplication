using MasrafTakipYönetim.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MasrafTakipYonetim.Domain.Entities
{
    public class AppUser : BaseEntity
    {

        [Required]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public byte[] PasswordHash { get; set; }

        [Required]
        public byte[] PasswordSalt { get; set; }

        [Phone]
        [StringLength(15)]
        public string PhoneNumber { get; set; }

        [NotMapped]
        public string Password { get; set; }
        [NotMapped]
        public string FullName { get { return this.FirstName + " " + this.LastName; } private set { } }
        public List<UserExpenseCategory> UserExpenseCategory { get; set; }

        [ForeignKey("Roles")]
        public Guid RoleId { get; set; }
        
       
        
        public Roles? Role { get; set; }
        

    }

}
