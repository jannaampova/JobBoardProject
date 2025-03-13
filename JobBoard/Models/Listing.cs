using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models
{
    public class Listing
    {
        [Key]
        public int Id { get; set; }
        public int companyId { get; set; }
        [ForeignKey("companyId")]
        public Company company { get; set; }
        public String Description { get; set; }
        public String title { get; set; }

        public DateOnly datePosted { get; set; }
        public int jobTypeId { get; set; }
        [ForeignKey("jobTypeId")]
        public JobType jobType { get; set; }
        [NotMapped]
        public List<ListingBenefits> listingBenefits { get; set; }
        [NotMapped]
        public List<ListingRequirements> listingRequirements { get; set; }

    }
}
