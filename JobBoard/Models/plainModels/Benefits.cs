using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models.plainModels
{
    public class Benefits
    {
        [Key]
        public int Id { get; set; }
        public string benefit { get; set; }
        [NotMapped]
        public List<ListingBenefits> benefits { get; set; }

    }
}
