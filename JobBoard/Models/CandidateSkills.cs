using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models
{
    public class CandidateSkills
    {
        public int CandidateId { get; set; }
        [ForeignKey("CandidateId")]
        public Candidate Candidate { get; set; }
        public int SkillId { get; set; }
        [ForeignKey("SkillId")]
        public Skills Skill { get; set; }
    }
}
