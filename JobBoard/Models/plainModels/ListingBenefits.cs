using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models.plainModels
{
    public class ListingBenefits
    {
        public int listingId { get; set; }
        [ForeignKey("listingId")]
        public Listing listing { get; set; }
        public int benefitId { get; set; }
        [ForeignKey("benefitId")]
        public Benefits benefit { get; set; }
    }
}
