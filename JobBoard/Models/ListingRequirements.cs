using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models
{
    public class ListingRequirements
    {
        public int listingId { get; set; }
        [ForeignKey("listingId")]
        public Listing listing { get; set; }
        public int requirementId { get; set; }
        [ForeignKey("requirementId")]
        public Requirements requirement { get; set; }
    }
}
