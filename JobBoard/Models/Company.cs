using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobBoard.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }

        public int accountId { get; set; }
        [ForeignKey("accountId")]
        public Account account { get; set; }
        public string webUrl { get; set; }
        public string location { get; set; }
        public string description { get; set; }
        public int industryId { get; set; }
        [ForeignKey("industryId")]
        public Industry industry { get; set; }
        public String photoPath {  get; set; }
        public String phone { get; set; }
    }
}
