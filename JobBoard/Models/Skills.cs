using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models
{
    public class Skills
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Skill { get; set; }
        [NotMapped]
        public List<CandidateSkills> skills { get; set; }


    }
}
