namespace JobBoard.Models
{
    public class Job
    {
        
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Employer { get; set; }
        //hello
            public DateTime PostedDate { get; set; } = DateTime.Now;
        
    }
}
