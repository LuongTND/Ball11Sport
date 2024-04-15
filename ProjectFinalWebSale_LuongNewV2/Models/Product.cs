using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Models
{
    public class Product
    {
        
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int UnitStock { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public string Images { get; set; }
        [Required]
        public bool Status { get; set; }
        

        public virtual Category Category { get; set; }
        
        public virtual User User { get; set; }

    }
}
