using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models.plainModels
{
    public class ListingRequirements
    {
        // Foreign key for Listing
        public int ListingId { get; set; }

        [ForeignKey("ListingId")]
        public Listing Listing { get; set; }

        // Foreign key for Requirement
        public int RequirementId { get; set; }

        [ForeignKey("RequirementId")]
        public Requirements Requirement { get; set; }
    }
}
