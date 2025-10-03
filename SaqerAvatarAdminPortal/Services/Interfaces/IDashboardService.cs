using SaqerAvatarAdminPortal.Models.Dashboard;

namespace SaqerAvatarAdminPortal.Services.Interfaces;

public interface IDashboardService
{
    /// <summary>
    /// Gets complete dashboard data for the specified date range
    /// </summary>
    /// <param name="fromDate">Start date for filtering (optional)</param>
    /// <param name="toDate">End date for filtering (optional)</param>
    /// <returns>Complete dashboard data</returns>
    Task<DashboardData> GetDashboardDataAsync(DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Gets statistics for the specified date range
    /// </summary>
    /// <param name="fromDate">Start date for filtering (optional)</param>
    /// <param name="toDate">End date for filtering (optional)</param>
    /// <returns>Dashboard statistics</returns>
    Task<Stats> GetStatsAsync(DateTime? fromDate = null, DateTime? toDate = null);

    /// <summary>
    /// Gets recent chats with optional count limit
    /// </summary>
    /// <param name="count">Number of recent chats to retrieve (default: 5)</param>
    /// <returns>List of recent chats</returns>
    Task<List<Chat>> GetRecentChatsAsync(int count = 5);

    /// <summary>
    /// Gets chat categories distribution
    /// </summary>
    /// <returns>Dictionary of category names and their counts</returns>
    Task<Dictionary<string, int>> GetCategoriesAsync();

    /// <summary>
    /// Gets daily chat volume data for the specified date range
    /// </summary>
    /// <param name="fromDate">Start date</param>
    /// <param name="toDate">End date</param>
    /// <returns>Daily chat volume data</returns>
    Task<DailyChats> GetDailyChatsAsync(DateTime fromDate, DateTime toDate);

    /// <summary>
    /// Gets daily success vs failed chat data for the specified date range
    /// </summary>
    /// <param name="fromDate">Start date</param>
    /// <param name="toDate">End date</param>
    /// <returns>Daily success/failed data</returns>
    Task<SuccessFailedDaily> GetSuccessFailedDailyAsync(DateTime fromDate, DateTime toDate);

    /// <summary>
    /// Gets daily ratings data for the specified date range
    /// </summary>
    /// <param name="fromDate">Start date</param>
    /// <param name="toDate">End date</param>
    /// <returns>Daily ratings data</returns>
    Task<RatingsDaily> GetRatingsDailyAsync(DateTime fromDate, DateTime toDate);

    /// <summary>
    /// Gets a specific chat by ID with full message history
    /// </summary>
    /// <param name="chatId">Chat identifier</param>
    /// <returns>Complete chat data or null if not found</returns>
    Task<Chat?> GetChatByIdAsync(int chatId);
}