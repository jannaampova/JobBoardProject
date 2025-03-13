using System.ComponentModel.DataAnnotations;

namespace JobBoard.Models
{
    public class JobType
    {
        [Key]
        public int Id { get; set; }
        public String jobType { get; set; }
    }
}
