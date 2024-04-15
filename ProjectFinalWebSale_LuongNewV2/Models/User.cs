
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class User
    {
        
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
       
        public string? Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Email { get; set; }

        public virtual Role Role { get; set; }


    }
}
