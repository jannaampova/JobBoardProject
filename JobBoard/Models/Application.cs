using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int candidateID { get; set; }
        [ForeignKey("candidateId")]
    }
}
