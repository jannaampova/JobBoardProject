using System.ComponentModel.DataAnnotations;

namespace JobBoard.Models
{
    public class Industry
    {
        [Key] public int Id { get; set; }
        public string industry {  get; set; }

    }
}
