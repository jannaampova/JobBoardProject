using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models
{
    public class Requirements
    {
        [Key]
        public int Id { get; set; }
        public String requirement { get; set; }
        [NotMapped]
        public List<ListingRequirements> listingRequirements { get; set; }
    }
}
