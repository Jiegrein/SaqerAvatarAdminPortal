using System.ComponentModel.DataAnnotations;

namespace SaqerAvatarAdminPortal.Models.Dashboard;

public class Stats
{
    [Display(Name = "Total Chats")]
    public int TotalChats { get; set; }

    [Display(Name = "Success Chats")]
    public int SuccessChats { get; set; }

    [Display(Name = "Failed Chats")]
    public int FailedChats { get; set; }

    [Display(Name = "Chats with Agent")]
    public int AgentChats { get; set; }

    /// <summary>
    /// Calculated property for success rate percentage
    /// </summary>
    public double SuccessRate => TotalChats > 0 ? Math.Round((double)SuccessChats / TotalChats * 100, 2) : 0;

    /// <summary>
    /// Calculated property for failure rate percentage
    /// </summary>
    public double FailureRate => TotalChats > 0 ? Math.Round((double)FailedChats / TotalChats * 100, 2) : 0;
}