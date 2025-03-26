using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public Account Account { get; set; } 
        public string PhotoPath { get; set; } 
        public string ResumePath { get; set; } 
        public CandidateEducation Education { get; set; } 
        public int yearsExperience { get; set; }

        [NotMapped]
        public List<CandidateSkills> candidateSkills { get; set; }


    }
}
