using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models.plainModels
{
    public class Listing
    {
        [Key]
        public int Id { get; set; }
        public int companyId { get; set; }
        [ForeignKey("companyId")]
        public Company company { get; set; }
        public string Description { get; set; }
        public string title { get; set; }

        public DateOnly datePosted { get; set; }

        public int jobTypeId { get; set; }
        [ForeignKey("jobTypeId")]
        public JobType jobType { get; set; }

        public int townId { get; set; }
        [ForeignKey("townId")]
        public Town town { get; set; }
        public string experienceLevel { get; set; }
        
        public string status { get; set; }


        [NotMapped]
        public List<ListingBenefits> listingBenefits { get; set; }
        [NotMapped]
        public List<ListingRequirements> listingRequirements { get; set; }
        
        public ICollection<Application> Applications { get; set; }



    }
}
