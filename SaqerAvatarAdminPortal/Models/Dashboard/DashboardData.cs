namespace SaqerAvatarAdminPortal.Models.Dashboard;

public class DashboardData
{
    public Stats Stats { get; set; } = new();
    public Dictionary<string, int> Categories { get; set; } = new();
    public DailyChats DailyChats { get; set; } = new();
    public SuccessFailedDaily SuccessFailedDaily { get; set; } = new();
    public RatingsDaily RatingsDaily { get; set; } = new();
    public List<Chat> RecentChats { get; set; } = new();
}