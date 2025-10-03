namespace SaqerAvatarAdminPortal.Models.Dashboard;

/// <summary>
/// Configuration settings for dashboard functionality
/// </summary>
public class DashboardSettings
{
    public const string SectionName = "Dashboard";
    
    /// <summary>
    /// Default number of recent chats to display
    /// </summary>
    public int DefaultRecentChatsCount { get; set; } = 5;
    
    /// <summary>
    /// Maximum allowed date range in days for filtering
    /// </summary>
    public int MaxDateRangeDays { get; set; } = 365;
    
    /// <summary>
    /// Default date range in days when no dates are specified
    /// </summary>
    public int DefaultDateRangeDays { get; set; } = 7;
    
    /// <summary>
    /// Enable/disable caching for dashboard data
    /// </summary>
    public bool EnableCaching { get; set; } = false;
    
    /// <summary>
    /// Cache duration in minutes
    /// </summary>
    public int CacheDurationMinutes { get; set; } = 15;
}