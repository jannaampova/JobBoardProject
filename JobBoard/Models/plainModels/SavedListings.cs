using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.Models.plainModels
{
    public class SavedListings
    {
        [Required]
        public int candidateId { get; set; }
        [ForeignKey("candidateId")]
        public Candidate candidate { get; set; }

        [Required]
        public int listingId { get; set; }
        [ForeignKey("listingId")]
        public Listing listing { get; set; }


    }
}
