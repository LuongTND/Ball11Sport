using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace Models
{
    public class Order
    {
        


        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public bool Status { get; set; }
        [Required]

        public virtual User User { get; set; }
    }
}
