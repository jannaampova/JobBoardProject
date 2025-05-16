using System.ComponentModel.DataAnnotations.Schema;


namespace JobBoard.Models.plainModels
{
    public class ListingBenefits
    {
        public int ListingId { get; set; }

        [ForeignKey("ListingId")]
        public Listing Listing { get; set; }

        public int BenefitId { get; set; }

        [ForeignKey("BenefitId")]
        public Benefits Benefit { get; set; }
    }
}

