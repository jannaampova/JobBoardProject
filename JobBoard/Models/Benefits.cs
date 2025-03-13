using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models
{
    public class Benefits
    {
        [Key]
        public int Id { get; set; }
        public String benefit { get; set; }
        [NotMapped]
        public List<ListingBenefits> benefits { get; set; }

    }
}
