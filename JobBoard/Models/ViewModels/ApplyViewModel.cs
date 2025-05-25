using System.Collections.Generic;
using JobBoard.Security;

namespace JobBoard.Models.ViewModels;
using JobBoard.Models.plainModels;
public class ApplyViewModel
{
    public UserData user { get; set; }
    public int AccountId { get; set; }
    public int CandidateId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string PhotoPath { get; set; }
    public string ResumePath { get; set; }
    public Listing jobListed { get; set; }
    public int JobId       { get; set; }
    public bool HasApplied { get; set; }
    public ApplyViewModel() {
    }
}