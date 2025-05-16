using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models.plainModels
{
    public class Candidate
    {
        public int Id { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string PhotoPath { get; set; }
        public string ResumePath { get; set; }
        public string Education { get; set; }
        public string ExperienceLevel { get; set; }
        [NotMapped]
        public List<CandidateSkills> candidateSkills { get; set; }
        
        public List<SavedListings> SavedListings { get; set; }


    }
}
