using System.ComponentModel.DataAnnotations;

namespace JobBoard.Models.plainModels
{
    public class Industry
    {
        [Key] public int Id { get; set; }
        public string industry { get; set; }

    }
}
