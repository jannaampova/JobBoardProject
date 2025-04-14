using System.ComponentModel.DataAnnotations;

namespace JobBoard.Models.plainModels
{
    public class JobType
    {
        [Key]
        public int Id { get; set; }
        public string jobType { get; set; }
    }
}
