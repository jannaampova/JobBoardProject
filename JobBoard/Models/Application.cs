using JobBoard.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CandidateId { get; set; }

        [Required]
        public int ListingId { get; set; }

        [Required]
        public ApplicationStatus Status { get; set; }

        [Required]
        public DateOnly AppliedAt { get; set; }

        [ForeignKey("CandidateId")]
        public virtual Candidate Candidate { get; set; }

        [ForeignKey("ListingId")]
        public virtual Listing Listing { get; set; }

    }
}
