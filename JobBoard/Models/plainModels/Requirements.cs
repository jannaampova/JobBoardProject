using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models.plainModels
{
    public class Requirements
    {
        [Key]
        public int Id { get; set; }
        public string requirement { get; set; }
        [NotMapped]
        public List<ListingRequirements> listingRequirements { get; set; }
    }
}
